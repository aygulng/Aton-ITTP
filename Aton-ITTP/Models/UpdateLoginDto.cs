using System.ComponentModel.DataAnnotations;

namespace Aton_ITTP.Models
{
    /// <summary>
    /// DTO для обновления логина пользователя
    /// </summary>
    public class UpdateLoginDto
    {
        /// <summary>
        /// Текущий логин пользователя (обязательное поле)
        /// </summary>
        [Required(ErrorMessage = "Current login is required")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Login must be between 3 and 50 characters")]
        required public string OldLogin { get; set; }

        /// <summary>
        /// Новый логин пользователя (обязательное поле)
        /// Должен содержать только латинские буквы и цифры
        /// </summary>
        [Required(ErrorMessage = "New login is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "Login can contain only latin letters and numbers")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Login must be between 3 and 50 characters")]
        required public string NewLogin { get; set; }
    }
}
