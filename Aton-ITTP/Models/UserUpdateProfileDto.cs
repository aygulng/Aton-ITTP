using System.ComponentModel.DataAnnotations;

namespace Aton_ITTP.Models
{
    /// <summary>
    /// DTO для обновления профиля пользователя
    /// </summary>
    public class UserUpdateProfileDto
    {
        /// <summary>
        /// Логин пользователя для обновления (обязательное поле)
        /// </summary>
        [Required(ErrorMessage = "Login is required")]
        required public string Login { get; set; }

        /// <summary>
        /// Новое имя пользователя (необязательное поле)
        /// Должно содержать только буквы (латиница или кириллица)
        /// </summary>
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$",
            ErrorMessage = "Name can contain only letters")]
        public string? Name { get; set; }

        /// <summary>
        /// Новый пол пользователя (необязательное поле)
        /// 0 - женский, 1 - мужской, 2 - неизвестно
        /// </summary>
        [Range(0, 2, ErrorMessage = "Gender must be 0, 1 or 2")]
        public int? Gender { get; set; }

        /// <summary>
        /// Новая дата рождения пользователя (необязательное поле)
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
    }
}
