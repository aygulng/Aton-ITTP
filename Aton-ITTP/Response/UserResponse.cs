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
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

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
        /// Дата и время создания пользователя
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Логин пользователя, создавшего запись
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Дата и время последнего изменения
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Логин пользователя, изменившего запись
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Дата и время удаления (null если пользователь активен)
        /// </summary>
        public DateTime? RevokedOn { get; set; }

        /// <summary>
        /// Логин пользователя, удалившего запись
        /// </summary>
        public string RevokedBy { get; set; }

        /// <summary>
        /// Конструктор для маппинга из сущности User
        /// </summary>
        public UserResponse(User user)
        {
            Id = user.Id;
            Login = user.Login;
            Name = user.Name;
            Gender = user.Gender
                .GetAttributeOfType<DescriptionAttribute>().Description;
            Birthday = user.Birthday;
            IsAdmin = user.Admin;
            CreatedOn = user.CreatedOn;
            CreatedBy = user.CreatedBy;
            ModifiedOn = user.ModifiedOn;
            ModifiedBy = user.ModifiedBy;
            RevokedOn = user.RevokedOn;
            RevokedBy = user.RevokedBy;
        }
    }
}
