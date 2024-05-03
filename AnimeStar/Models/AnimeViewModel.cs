using System.ComponentModel.DataAnnotations;

namespace AnimeStar.Models
{
    public class AnimeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата выхода")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Рейтинг MPAA")]
        public int MpaaId { get; set; }

        [Display(Name = "Тип аниме")]
        public string TypeOfAnime { get; set; }

        [Display(Name = "Количество серий")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество серий должно быть больше нуля")]
        public int NumberOfEpisodes { get; set; }

        [Display(Name = "Продолжительность фильма")]
        public string? LenghtOfTheFilm { get; set; }

        [Display(Name = "Состояние")]
        public string AnimeState { get; set; }

        [Display(Name = "Средний рейтинг")]
        public double AverageRating { get; set; }
        [Display(Name = "Изображение")]
        public IFormFile? ImageFile { get; set; }
        public List<int>? GenreIds { get; set; }
        public List<int>? CharacterIds { get; set; }
        public int? StudioId { get; set; }
    }
}
