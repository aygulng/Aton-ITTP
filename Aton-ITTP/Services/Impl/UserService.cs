using Aton_ITTP.DataAccess;
using Aton_ITTP.Entities;
using Aton_ITTP.Enums;
using Aton_ITTP.Models;
using Aton_ITTP.Response;
using Aton_ITTP.Utils;
using Microsoft.EntityFrameworkCore;

namespace Aton_ITTP.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UserService> _log;

        public UserService(AppDbContext db, ILogger<UserService> log)
        {
            _db = db;
            _log = log;
        }

        /// <inheritdoc />
        public async Task<Result> CreateUserAsync(
            UserCreateDto dto, 
            string adminLogin, 
            string adminPassword)
        {
            var admin = await _db.Set<User>()
                .Where(x => x.Login == adminLogin)
                .Where(x => x.Password == adminPassword)
                .Where(x => x.Admin)
                .Where(x => x.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                _log.LogWarning("Unauthorized attempt to create user by {Login}", adminLogin);
                return Result.WithError("Only admins can create users");
            }

            var userExists = await _db.Set<User>()
                .AnyAsync(x => x.Login == dto.Login);
            
            if (userExists)
            {
                return Result.WithError("Login already exists");
            }

            var user = new User
            {
                Login = dto.Login,
                Password = dto.Password,
                Name = dto.Name,
                Gender = dto.Gender,
                Birthday = dto.Birthday,
                Admin = dto.IsAdmin,
                CreatedBy = admin.Login,
                ModifiedBy = admin.Login
            };

            await _db.Set<User>().AddAsync(user);
            await _db.SaveChangesAsync();

            _log.LogInformation("User {Login} created by admin {Admin}", dto.Login, adminLogin);
            return Result.WithData();
        }

        /// <inheritdoc />
        public async Task<Result> UpdateUserProfileAsync(
            UserUpdateProfileDto dto,
            string currentUserLogin,
            string currentUserPassword)
        {
            var currentUser = await _db.Set<User>()
                .Where(x => x.Login == currentUserLogin)
                .Where(x => x.Password == currentUserPassword)
                .Where(x => x.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (currentUser == null)
                return Result.WithError("Invalid credentials");

            var userToUpdate = await _db.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (userToUpdate == null)
                return Result.WithError("User not found");

            if (userToUpdate.RevokedOn != null)
                return Result.WithError("User is revoked");

            if (!currentUser.Admin && currentUser.Login != userToUpdate.Login)
                return Result.WithError("No permissions to update this user");

            userToUpdate.Name = dto.Name ?? userToUpdate.Name;
            userToUpdate.Gender = dto.Gender != null
                ? (GenderType)dto.Gender
                : userToUpdate.Gender;
            userToUpdate.Birthday = dto.Birthday ?? userToUpdate.Birthday;
            userToUpdate.ModifiedOn = DateTime.UtcNow;
            userToUpdate.ModifiedBy = currentUserLogin;

            await _db.SaveChangesAsync();
            return Result.WithData();
        }

        /// <inheritdoc />
        public async Task<Result> UpdatePasswordAsync(
            UpdatePasswordDto dto,
            string currentUserLogin,
            string currentUserPassword)
        {
            var currentUser = await _db.Set<User>()
                .Where(x => x.Login == currentUserLogin)
                .Where(x => x.Password == currentUserPassword)
                .Where(x => x.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (currentUser == null)
                return Result.WithError("Invalid credentials");

            var userToUpdate = await _db.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (userToUpdate == null)
                return Result.WithError("User not found");

            if (userToUpdate.RevokedOn != null)
                return Result.WithError("User is revoked");

            if (!currentUser.Admin && currentUser.Login != userToUpdate.Login)
                return Result.WithError("No permissions to update this user");

            userToUpdate.Password = dto.NewPassword;
            userToUpdate.ModifiedOn = DateTime.UtcNow;
            userToUpdate.ModifiedBy = currentUserLogin;

            await _db.SaveChangesAsync();
            return Result.WithData();
        }

        /// <inheritdoc />
        public async Task<Result<List<UserResponse>>> GetAllActiveUsersAsync(
            string adminLogin,
            string adminPassword)
        {
            var admin = await _db.Set<User>()
                .Where(x => x.Login == adminLogin)
                .Where(x => x.Password == adminPassword)
                .Where(x => x.Admin)
                .Where(x => x.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
                return Result<List<UserResponse>>.WithError("Admin access required");

            var users = await _db.Set<User>()
                .Where(x => x.RevokedOn == null)
                .OrderBy(x => x.CreatedOn)
                .Select(x => new UserResponse(x))
                .ToListAsync();

            return Result<List<UserResponse>>.WithData(users);
        }

        /// <inheritdoc />
        public async Task<Result> SoftDeleteUserAsync(
            string login,
            string revokedByLogin,
            string revokedByPassword)
        {
            var admin = await _db.Set<User>()
                .Where(x => x.Login == revokedByLogin)
                .Where(x => x.Password == revokedByPassword)
                .Where(x => x.Admin)
                .Where(x => x.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
                return Result.WithError("Admin access required");

            var user = await _db.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == login);

            if (user == null)
                return Result.WithError("User not found");

            user.RevokedOn = DateTime.UtcNow;
            user.RevokedBy = revokedByLogin;
            user.ModifiedOn = DateTime.UtcNow;
            user.ModifiedBy = revokedByLogin;

            await _db.SaveChangesAsync();
            return Result.WithData();
        }

        /// <inheritdoc />
        public async Task<Result> RestoreUserAsync(
            string login,
            string restoredByLogin,
            string restoredByPassword)
        {
            var admin = await _db.Set<User>()
                .Where(x => x.Login == restoredByLogin)
                .Where(x => x.Password == restoredByPassword)
                .Where(x => x.Admin)
                .Where(x => x.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
                return Result.WithError("Admin access required");

            var user = await _db.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == login);

            if (user == null)
                return Result.WithError("User not found");

            user.RevokedOn = null;
            user.RevokedBy = string.Empty;
            user.ModifiedOn = DateTime.UtcNow;
            user.ModifiedBy = restoredByLogin;

            await _db.SaveChangesAsync();
            return Result.WithData();
        }

        public async Task<Result> UpdateLoginAsync(
            UpdateLoginDto dto,
            string currentUserLogin,
            string currentUserPassword)
        {
            var currentUser = await _db.Set<User>()
                .Where(u => u.Login == currentUserLogin && u.Password == currentUserPassword && u.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (currentUser == null)
            {
                _log.LogWarning("Unauthorized login update attempt by {Login}", currentUserLogin);
                return Result.WithError("Invalid credentials or user is revoked");
            }

            var userToUpdate = await _db.Set<User>()
                .FirstOrDefaultAsync(u => u.Login == dto.OldLogin);

            if (userToUpdate == null)
                return Result.WithError("User not found");

            if (!currentUser.Admin && currentUser.Login != userToUpdate.Login)
                return Result.WithError("No permission to update this user's login");

            if (userToUpdate.RevokedOn != null)
                return Result.WithError("Cannot update login for revoked user");

            if (await _db.Set<User>().AnyAsync(u => u.Login == dto.NewLogin))
                return Result.WithError("New login is already taken");

            userToUpdate.Login = dto.NewLogin;
            userToUpdate.ModifiedOn = DateTime.UtcNow;
            userToUpdate.ModifiedBy = currentUserLogin;

            await _db.SaveChangesAsync();
            _log.LogInformation("Login updated for user {OldLogin} to {NewLogin}", dto.OldLogin, dto.NewLogin);
            return Result.WithData();
        }

        public async Task<Result<UserResponse>> GetUserByLoginAsync(
            string login,
            string adminLogin,
            string adminPassword)
        {
            var admin = await _db.Set<User>()
                .Where(u => u.Login == adminLogin && u.Password == adminPassword && u.Admin && u.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                _log.LogWarning("Unauthorized user info request by {Login}", adminLogin);
                return Result<UserResponse>.WithError("Admin access required");
            }

            var user = await _db.Set<User>()
                .Where(u => u.Login == login)
                .Select(u => new UserResponse(u))
                .FirstOrDefaultAsync();

            if (user == null)
                return Result<UserResponse>.WithError("User not found");

            return Result<UserResponse>.WithData(user);
        }

        public async Task<Result<UserResponse>> GetCurrentUserAsync(
            string login,
            string password)
        {
            var user = await _db.Set<User>()
                .Where(u => u.Login == login && u.Password == password && u.RevokedOn == null)
                .Select(u => new UserResponse(u))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                _log.LogWarning("Failed login attempt for {Login}", login);
                return Result<UserResponse>.WithError("Invalid credentials or user is revoked");
            }

            return Result<UserResponse>.WithData(user);
        }

        public async Task<Result<List<UserResponse>>> GetUsersOlderThanAsync(
            int age,
            string adminLogin,
            string adminPassword)
        {
            var admin = await _db.Set<User>()
                .Where(u => u.Login == adminLogin && u.Password == adminPassword && u.Admin && u.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                _log.LogWarning("Unauthorized age filter request by {Login}", adminLogin);
                return Result<List<UserResponse>>.WithError("Admin access required");
            }

            // Вычисляем минимальную дату рождения
            var minBirthDate = DateTime.UtcNow.AddYears(-age);

            // Получаем пользователей старше указанного возраста
            var users = await _db.Set<User>()
                .Where(u => u.Birthday != null && u.Birthday <= minBirthDate)
                .OrderByDescending(u => u.Birthday)
                .Select(u => new UserResponse(u))
                .ToListAsync();

            return Result<List<UserResponse>>.WithData(users);
        }

        public async Task<Result> DeleteUserAsync(
            string login,
            bool hardDelete,
            string adminLogin,
            string adminPassword)
        {
            var admin = await _db.Set<User>()
                .Where(u => u.Login == adminLogin 
                && u.Password == adminPassword 
                && u.Admin 
                && u.RevokedOn == null)
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                _log.LogWarning("Unauthorized delete attempt by {Login}", adminLogin);
                return Result.WithError("Admin access required");
            }

            var user = await _db.Set<User>()
                .FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
                return Result.WithError("User not found");

            if (hardDelete)
            {
                _db.Set<User>().Remove(user);
                _log.LogInformation("User {Login} hard deleted by {Admin}", login, adminLogin);
            }
            else
            {
                user.RevokedOn = DateTime.UtcNow;
                user.RevokedBy = adminLogin;
                user.ModifiedOn = DateTime.UtcNow;
                user.ModifiedBy = adminLogin;
                _log.LogInformation("User {Login} soft deleted by {Admin}", login, adminLogin);
            }

            await _db.SaveChangesAsync();
            return Result.WithData();
        }
    }
}
