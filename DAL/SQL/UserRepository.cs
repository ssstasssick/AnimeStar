using DAL.Entity;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQL
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);

            if (existingUser != null)
            {
                // Проверяем текущий пароль
                var result = await _userManager.CheckPasswordAsync(existingUser, currentPassword);
                if (!result)
                {
                    throw new InvalidOperationException("Текущий пароль неверен.");
                }

                // Пытаемся изменить пароль
                var changePasswordResult = await _userManager.ChangePasswordAsync(existingUser, currentPassword, newPassword);
                if (!changePasswordResult.Succeeded)
                {
                    // Получаем сообщения об ошибках
                    var errors = string.Join(", ", changePasswordResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Не удалось изменить пароль: {errors}");
                }
            }
        }


        public async Task Create(ApplicationUser user, string password, string role) 
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                throw new InvalidOperationException($"Failed to create user: {errors}");
            }
            await EnsureRolesCreated();
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task Delete(ApplicationUser user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                await _userManager.DeleteAsync(existingUser);
            }
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await Task.FromResult<IEnumerable<ApplicationUser>>(_userManager.Users);
        }

        public async Task<bool> SignIn(string name, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(name, password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to sign in.");
            }
            return result.Succeeded;
        }

        public async Task<ApplicationUser>FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task EnsureRolesCreated()
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                var adminRole = new IdentityRole { Name = "admin" };
                await _roleManager.CreateAsync(adminRole);
            }

            if (!await _roleManager.RoleExistsAsync("moder"))
            {
                var moderRole = new IdentityRole { Name = "moder" };
                await _roleManager.CreateAsync(moderRole);
            }

            if (!await _roleManager.RoleExistsAsync("default"))
            {
                var moderRole = new IdentityRole { Name = "default" };
                await _roleManager.CreateAsync(moderRole);
            }
        }

        public async Task<List<string>> GetUserRolesAsync(string userName)
        {
            // Получаем пользователя по его имени
            var user = await _userManager.FindByNameAsync(userName);

            // Если пользователь найден, получаем его роли
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToList();
            }

            // Если пользователь не найден, возвращаем пустой список ролей
            return new List<string>();
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                await _userManager.UpdateAsync(existingUser);
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            
            return await _userManager.GetRolesAsync(user);
        }

        public async Task RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var nuser = await _userManager.FindByIdAsync(user.Id);
            await _userManager.RemoveFromRolesAsync(nuser, roles);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            var nuser = await _userManager.FindByIdAsync(user.Id);
            await _userManager.AddToRoleAsync(nuser, role);
        }

    }
}
