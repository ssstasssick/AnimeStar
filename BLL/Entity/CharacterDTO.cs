﻿namespace BLL.Entity
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgName { get; set; }
        public virtual ICollection<AnimeDTO> Animes { get; set; }

    }
}
