using Aton_ITTP.Models;
using Aton_ITTP.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aton_ITTP.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Создание пользователя (только для админов)
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(
            [FromBody] UserCreateDto dto,
            [FromQuery] string adminLogin,
            [FromQuery] string adminPassword)
        {
            var result = await _userService.CreateUserAsync(dto, adminLogin, adminPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Create user failed: {Error}", result.Error);
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Изменение профиля пользователя
        /// </summary>
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile(
            [FromBody] UserUpdateProfileDto dto,
            [FromQuery] string currentUserLogin,
            [FromQuery] string currentUserPassword)
        {
            var result = await _userService.UpdateUserProfileAsync(dto, currentUserLogin, currentUserPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Update profile failed for {Login}: {Error}", dto.Login, result.Error);
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Изменение пароля пользователя
        /// </summary>
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordDto dto,
            [FromQuery] string currentUserLogin,
            [FromQuery] string currentUserPassword)
        {
            var result = await _userService.UpdatePasswordAsync(dto, currentUserLogin, currentUserPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Password update failed for {Login}: {Error}", dto.Login, result.Error);
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Изменение логина пользователя
        /// </summary>
        [HttpPut("update-login")]
        public async Task<IActionResult> UpdateLogin(
            [FromBody] UpdateLoginDto dto,
            [FromQuery] string currentUserLogin,
            [FromQuery] string currentUserPassword)
        {
            var result = await _userService.UpdateLoginAsync(dto, currentUserLogin, currentUserPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Login update failed from {Old} to {New}: {Error}",
                    dto.OldLogin, dto.NewLogin, result.Error);
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Получение списка активных пользователей (только для админов)
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveUsers(
            [FromQuery] string adminLogin,
            [FromQuery] string adminPassword)
        {
            var result = await _userService.GetAllActiveUsersAsync(adminLogin, adminPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Get active users failed: {Error}", result.Error);
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Получение пользователя по логину (только для админов)
        /// </summary>
        [HttpGet("by-login/{login}")]
        public async Task<IActionResult> GetUserByLogin(
            string login,
            [FromQuery] string adminLogin,
            [FromQuery] string adminPassword)
        {
            var result = await _userService.GetUserByLoginAsync(login, adminLogin, adminPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Get user by login failed for {Login}: {Error}", login, result.Error);
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Получение информации о текущем пользователе
        /// </summary>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser(
            [FromQuery] string login,
            [FromQuery] string password)
        {
            var result = await _userService.GetCurrentUserAsync(login, password);
            if (!result.Success)
            {
                _logger.LogWarning("Get current user failed for {Login}: {Error}", login, result.Error);
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Получение пользователей старше указанного возраста (только для админов)
        /// </summary>
        [HttpGet("older-than/{age}")]
        public async Task<IActionResult> GetUsersOlderThan(
            int age,
            [FromQuery] string adminLogin,
            [FromQuery] string adminPassword)
        {
            var result = await _userService.GetUsersOlderThanAsync(age, adminLogin, adminPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Get users older than {Age} failed: {Error}", age, result.Error);
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Удаление пользователя (только для админов)
        /// </summary>
        [HttpDelete("{login}")]
        public async Task<IActionResult> DeleteUser(
            string login,
            [FromQuery] bool hardDelete,
            [FromQuery] string adminLogin,
            [FromQuery] string adminPassword)
        {
            var result = await _userService.DeleteUserAsync(login, hardDelete, adminLogin, adminPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Delete user failed for {Login}: {Error}", login, result.Error);
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Восстановление пользователя (только для админов)
        /// </summary>
        [HttpPut("restore/{login}")]
        public async Task<IActionResult> RestoreUser(
            string login,
            [FromQuery] string adminLogin,
            [FromQuery] string adminPassword)
        {
            var result = await _userService.RestoreUserAsync(login, adminLogin, adminPassword);
            if (!result.Success)
            {
                _logger.LogWarning("Restore user failed for {Login}: {Error}", login, result.Error);
                return BadRequest(result.Error);
            }
            return Ok();
        }
    }
}
