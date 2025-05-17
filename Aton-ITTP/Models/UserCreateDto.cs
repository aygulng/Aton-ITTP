using Aton_ITTP.Enums;
using System.ComponentModel.DataAnnotations;

namespace Aton_ITTP.Models
{
    /// <summary>
    /// DTO для создания нового пользователя
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Логин пользователя (обязательное поле)
        /// Должен содержать только латинские буквы и цифры
        /// Должен быть уникальным в системе
        /// </summary>
        [Required(ErrorMessage = "Login is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "Login can contain only latin letters and numbers")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Login must be between 3 and 50 characters")]
        required public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя (обязательное поле)
        /// Должен содержать только латинские буквы и цифры
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "Password can contain only latin letters and numbers")]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "Password must be between 6 and 100 characters")]
        required public string Password { get; set; }

        /// <summary>
        /// Имя пользователя (обязательное поле)
        /// Должно содержать только буквы (латиница или кириллица)
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s]+$",
            ErrorMessage = "Name can contain only letters")]
        [StringLength(100,
            ErrorMessage = "Name cannot exceed 100 characters")]
        required public string Name { get; set; }

        /// <summary>
        /// Пол пользователя (обязательное поле)
        /// 0 - Женский, 1 - Мужской, 2 - Неизвестно
        /// </summary>
        [Required(ErrorMessage = "Gender is required")]
        [Range(0, 2, ErrorMessage = "Gender must be 0, 1 or 2")]
        required public GenderType Gender { get; set; }

        /// <summary>
        /// Дата рождения пользователя (необязательное поле)
        /// Должна быть не позднее текущей даты
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Флаг, указывающий является ли пользователь администратором
        /// По умолчанию: false
        /// </summary>
        public bool IsAdmin { get; set; } = false;
    }
}
