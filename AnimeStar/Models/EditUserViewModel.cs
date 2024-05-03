using System.ComponentModel.DataAnnotations;

namespace AnimeStar.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Текущее имя")]
        public string? CurrentUserName { get; set; }

        [Display(Name = "Новое имя")]
        public string? NewUserName { get; set; }

        [Display(Name = "Новый пароль")]
        public string? NewPassword { get; set; }
        [Display(Name = "Старый пароль")]
        public string? OldPassword { get; set; }
        [Display(Name = "Изменить роль")]
        public string? NewRole { get; set; }
    }
}
