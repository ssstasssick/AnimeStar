namespace BLL.Entity
{
    public class StudioDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AnimeDTO> Animes { get; set; }
    }
}
