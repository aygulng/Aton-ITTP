using Aton_ITTP.Entities;
using Aton_ITTP.Models;
using Aton_ITTP.Response;
using Aton_ITTP.Utils;

namespace Aton_ITTP.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Проверяет учетные данные и возвращает администратора, если он существует.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Результат с объектом администратора или ошибкой.</returns>
        Task<Result<User>> GetAdminAsync(string login, string password);

        /// <summary>
        /// Получает пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        Task<Result<User>> AuthenticateUserAsync(
            string login,
            string password);

        /// <summary>
        /// Создание пользователя по логину, паролю, имени, полу и дате рождения 
        /// + указание будет ли пользователь админом (Доступно Админам)
        /// </summary>
        /// <param name="dto">DTO с данными для создания пользователя</param>
        /// <param name="adminLogin">Логин администратора</param>
        /// <param name="adminPassword">Пароль администратора</param>
        /// <returns>Созданный пользователь</returns>
        Task<Result> CreateUserAsync(
            UserCreateDto dto,
            string adminLogin,
            string adminPassword);

        /// <summary>
        /// Изменение имени, пола или даты рождения пользователя
        /// (Может менять Администратор либо лично пользователь, если он активен)
        /// </summary>
        /// <param name="dto">DTO с обновляемыми данными</param>
        /// <param name="currentUserLogin">Логин текущего пользователя</param>
        /// <param name="currentUserPassword">Пароль текущего пользователя</param>
        /// <returns>Результат операции</returns>
        Task<Result> UpdateUserProfileAsync(
            UserUpdateProfileDto dto,
            string currentUserLogin,
            string currentUserPassword);

        /// <summary>
        /// Изменение пароля
        /// (Пароль может менять либо Администратор, либо лично пользователь, если он активен)
        /// </summary>
        /// <param name="dto">DTO с новым паролем</param>
        /// <param name="currentUserLogin">Логин текущего пользователя</param>
        /// <param name="currentUserPassword">Пароль текущего пользователя</param>
        /// <returns>Результат операции</returns>
        Task<Result> UpdatePasswordAsync(
            UpdatePasswordDto dto,
            string currentUserLogin,
            string currentUserPassword);

        /// <summary>
        /// Изменение логина
        /// (Логин может менять либо Администратор, либо лично пользователь, если он активен)
        /// </summary>
        /// <param name="dto">DTO с новым логином</param>
        /// <param name="currentUserLogin">Логин текущего пользователя</param>
        /// <param name="currentUserPassword">Пароль текущего пользователя</param>
        /// <returns>Результат операции</returns>
        Task<Result> UpdateLoginAsync(
            UpdateLoginDto dto,
            string currentUserLogin,
            string currentUserPassword);

        /// <summary>
        /// Запрос списка всех активных пользователей
        /// (отсортирован по CreatedOn, Доступно Админам)
        /// </summary>
        /// <param name="adminLogin">Логин администратора</param>
        /// <param name="adminPassword">Пароль администратора</param>
        /// <returns>Список активных пользователей</returns>
        Task<Result<List<UserResponse>>> GetAllActiveUsersAsync(
            string adminLogin,
            string adminPassword);

        /// <summary>
        /// Запрос пользователя по логину
        /// (Доступно Админам)
        /// </summary>
        /// <param name="login">Логин искомого пользователя</param>
        /// <param name="adminLogin">Логин администратора</param>
        /// <param name="adminPassword">Пароль администратора</param>
        /// <returns>Информация о пользователе</returns>
        Task<Result<UserResponse>> GetUserByLoginAsync(
            string login,
            string adminLogin,
            string adminPassword);

        /// <summary>
        /// Запрос пользователя по логину и паролю
        /// (Доступно только самому пользователю, если он активен)
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Информация о текущем пользователе</returns>
        Task<Result<UserResponse>> GetCurrentUserAsync(
            string login,
            string password);

        /// <summary>
        /// Запрос всех пользователей старше определённого возраста
        /// (Доступно Админам)
        /// </summary>
        /// <param name="age">Минимальный возраст</param>
        /// <param name="adminLogin">Логин администратора</param>
        /// <param name="adminPassword">Пароль администратора</param>
        /// <returns>Список пользователей старше указанного возраста</returns>
        Task<Result<List<UserResponse>>> GetUsersOlderThanAsync(
            int age,
            string adminLogin,
            string adminPassword);

        /// <summary>
        /// Удаление пользователя по логину (мягкое или полное)
        /// (При мягком удалении проставляется RevokedOn и RevokedBy, Доступно Админам)
        /// </summary>
        /// <param name="login">Логин удаляемого пользователя</param>
        /// <param name="hardDelete">Флаг полного удаления</param>
        /// <param name="adminLogin">Логин администратора</param>
        /// <param name="adminPassword">Пароль администратора</param>
        /// <returns>Результат операции</returns>
        Task<Result> DeleteUserAsync(
            string login,
            bool hardDelete,
            string adminLogin,
            string adminPassword);

        /// <summary>
        /// Восстановление пользователя - очистка полей RevokedOn, RevokedBy
        /// (Доступно Админам)
        /// </summary>
        /// <param name="login">Логин восстанавливаемого пользователя</param>
        /// <param name="adminLogin">Логин администратора</param>
        /// <param name="adminPassword">Пароль администратора</param>
        /// <returns>Результат операции</returns>
        Task<Result> RestoreUserAsync(
            string login,
            string adminLogin,
            string adminPassword);

    }
}
