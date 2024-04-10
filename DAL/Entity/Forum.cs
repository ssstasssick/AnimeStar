using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Entity
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
    }
}
