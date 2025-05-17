using Aton_ITTP.Entities;
using Microsoft.OpenApi.Extensions;
using System.ComponentModel;

namespace Aton_ITTP.Response
{
    /// <summary>
    /// Модель ответа с информацией о пользователе
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пол пользователя
        /// 0 - Женский, 1 - Мужской, 2 - Неизвестно
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Дата рождения пользователя (может быть null)
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Флаг, указывающий является ли пользователь администратором
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public string UserStatus { get; set; }

        /// <summary>
        /// Конструктор для маппинга из сущности User
        /// </summary>
        public UserResponse(User user)
        {
            Login = user.Login;
            Name = user.Name;
            Gender = user.Gender.GetAttributeOfType<DescriptionAttribute>().Description;
            Birthday = user.Birthday;
            UserStatus = user.RevokedOn.HasValue ? "Inactive" : "Active";
            IsAdmin = user.Admin;           
        }
    }
}
