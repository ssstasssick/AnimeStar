namespace BLL.Entity
{
    public class AnimeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MPAAId { get; set; }
        public MPAA_DTO MPAA{ get; set; }
        public string PictureName { get; set; }
        public string TypeOfAnime { get; set; }
        public int NumberOfEpisodes { get; set; }
        public string LenghtOfTheFilm { get; set; }
        public string AnimeState { get; set; }
        public string ImgPath { get; set; }
        public virtual ICollection<CharacterDTO> Characters { get; set; }
        public virtual ICollection<GenreDTO> Genres { get; set; }
        public virtual ICollection<StudioDTO> Studios { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
        public virtual ICollection<ForumDTO> Forums { get; set; }
        public virtual ICollection<PersonalListDTO> PersonalLists { get; set; }


    }
}
