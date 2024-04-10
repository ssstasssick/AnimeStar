using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgName { get; set; }
        public virtual ICollection<AnimeAndCharacter> AnimeAndCharacters { get; set; }

    }
}
