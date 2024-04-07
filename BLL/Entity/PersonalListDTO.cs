namespace BLL.Entity
{
    public enum State
    {
        Запланировано,
        Смотрю,
        Просмотрено,
        Отложено,
        Брошено
    }
    public class PersonalListDTO
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public AnimeDTO Anime { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public State State { get; set; }
    }
}
