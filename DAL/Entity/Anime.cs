using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Anime
    {
        enum AnimeType
        {
            Фильм,
            Сериал
        }

        enum State
        {
            Вышло,
            Онгоинг
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MPAAId { get; set; }
        public MPAA MPAA{ get; set; }
        public string PictureName {  get; set; }
        [EnumDataType(typeof(AnimeType))]
        public string TypeOfAnime { get; set; }
        public int NumberOfEpisodes { get; set; }
        public string? LenghtOfTheFilm { get; set; }
        [EnumDataType(typeof(State))]
        public string AnimeState { get; set; }

        public double AverageRating { get; set; } = 0.0;
        public virtual ICollection<AnimeAndCharacter> AnimeAndCharacters { get; set; }
        public virtual ICollection<AnimeAndGenre> AnimeAndGenres { get; set; }
        public virtual ICollection<AnimeAndStudio> AnimeAndStudios { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Forum> Forums { get; set; }
        public virtual ICollection<PersonalList> PersonalLists { get; set; }


    }
}
