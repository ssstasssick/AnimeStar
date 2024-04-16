using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ForumId { get; set; }
        public Forum Forum { get; set; }
    }
}
