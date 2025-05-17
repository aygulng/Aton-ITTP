using System.ComponentModel.DataAnnotations;

namespace Aton_ITTP.Models
{
    /// <summary>
    /// DTO для обновления пароля пользователя
    /// </summary>
    public class UpdatePasswordDto
    {
        /// <summary>
        /// Логин пользователя для смены пароля (обязательное поле)
        /// </summary>
        [Required(ErrorMessage = "Login is required")]
        required public string Login { get; set; }

        /// <summary>
        /// Новый пароль (обязательное поле)
        /// Должен содержать только латинские буквы и цифры
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "Password can contain only letters and numbers")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        required public string NewPassword { get; set; }
    }
}
