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
        Task ChangePassword(ApplicationUser user, string password);
        Task Create(ApplicationUser user, string password);
        Task Delete(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> FindByEmailAsync(string email);

    }

}
