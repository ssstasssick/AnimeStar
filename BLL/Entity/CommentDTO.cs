namespace BLL.Entity
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public string UserName { get; set; }
        public int? ForumId { get; set; }
        public ForumDTO Forum { get; set; }
        public int AnimeId { get; set; }
        public AnimeDTO Anime { get; set; }
    }
}
