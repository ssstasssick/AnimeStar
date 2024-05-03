using System.ComponentModel.DataAnnotations;

namespace AnimeStar.Models
{
    public class CharacterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public IFormFile? ImageFile { get; set; }

    }
}
