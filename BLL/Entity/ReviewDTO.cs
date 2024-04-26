namespace BLL.Entity
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int? Rating { get; set; }
        public string UserId {  get; set; }
        public UserDTO User { get; set; }
        public int AnimeId { get; set; }
        public AnimeDTO Anime { get; set; }
    }
}
