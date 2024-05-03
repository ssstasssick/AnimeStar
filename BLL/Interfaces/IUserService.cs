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
        Task CreateUser(UserDTO user, string password, string role);
        Task ChangePassword(UserDTO user, string oldPassword, string newPassword);
        Task DeleteUser(UserDTO  user);
        Task<UserDTO> GetUserByEmail(string email);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<bool> SignIn(string email, string password);
        Task<UserDTO> FindByName(string username);
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<List<string>> GetUserRolesAsync(string userName);
        Task UpdateUser(UserDTO user);
        Task<IList<string>> GetRolesAsync(UserDTO user);
        Task RemoveFromRolesAsync(UserDTO user, IEnumerable<string> roles);
        Task AddToRoleAsync(UserDTO user, string role);
    }
}
