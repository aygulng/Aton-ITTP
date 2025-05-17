using Aton_ITTP.Enums;

namespace Aton_ITTP.Entities
{
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя (генерируется автоматически)
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Уникальный логин пользователя (только латинские буквы и цифры)
        /// </summary>
        required public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя (только латинские буквы и цифры)
        /// </summary>
        required public string Password { get; set; }

        /// <summary>
        /// Имя пользователя (только латинские и русские буквы)
        /// </summary>
        required public string Name { get; set; }

        /// <summary>
        /// Пол: 0 - женский, 1 - мужской, 2 - неизвестно
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Дата рождения пользователя (может быть null)
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Флаг, указывающий является ли пользователь администратором
        /// </summary>
        public bool Admin { get; set; }

        /// <summary>
        /// Дата и время создания пользователя (устанавливается автоматически)
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Логин пользователя, создавшего данную запись
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Дата и время последнего изменения пользователя (обновляется автоматически)
        /// </summary>
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Логин пользователя, изменившего данную запись
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Дата и время удаления/блокировки пользователя (null если активен)
        /// </summary>
        public DateTime? RevokedOn { get; set; }

        /// <summary>
        /// Логин пользователя, удалившего/заблокировавшего данную запись
        /// </summary>
        public string RevokedBy { get; set; }
    }
}
