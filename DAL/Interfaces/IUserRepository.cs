using DAL.Entity;
using DAL.SQL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task ChangePassword(ApplicationUser user, string oldPassword, string password);
        Task Create(ApplicationUser user, string password, string role);
        Task Delete(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> SignIn(string email, string password);
        Task<ApplicationUser> FindByNameAsync(string name);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task EnsureRolesCreated();
        Task<List<string>> GetUserRolesAsync(string userName);
        Task UpdateUser(ApplicationUser user);

        Task AddToRoleAsync(ApplicationUser user, string role);
        Task RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);



    }

}
