using System.ComponentModel.DataAnnotations;

namespace AnimeStar.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Имя")]
        public string Name { get; set; }
    }
}
