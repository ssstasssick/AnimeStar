namespace BLL.Entity
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public UserDTO UserDTO { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }

        public int AnimeId { get; set; }
        public AnimeDTO Anime { get; set; }
    }
}
