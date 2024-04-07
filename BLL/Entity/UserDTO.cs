using Microsoft.AspNetCore.Identity;
namespace BLL.Entity
{
    public class UserDTO : IdentityUser
    {
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }  
        public DateTime RegisterDate { get; set; }


    }
}
