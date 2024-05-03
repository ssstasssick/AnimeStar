namespace BLL.Entity
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }

        public int AnimeId { get; set; }
        public AnimeDTO Anime { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
