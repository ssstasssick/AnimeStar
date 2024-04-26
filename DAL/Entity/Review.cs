using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int? Rating { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
    }
}
