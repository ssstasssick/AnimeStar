using BLL.Entity;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO user, string password);
        Task ChangePassword(UserDTO user, string newPassword);
        Task DeleteUser(UserDTO  user);
        Task<UserDTO> GetUserByEmail(string email);
        Task<IEnumerable<UserDTO>> GetAllUsers();
    }
}
