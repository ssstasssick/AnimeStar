using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
using DAL.Entity;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateUser(UserDTO user, string password, string role)
        {
            await userRepository.Create(_mapper.Map<ApplicationUser>(user), password, role);
        }

        public async Task ChangePassword(UserDTO user, string oldPassword, string newPassword)
        {
            await userRepository.ChangePassword(_mapper.Map<ApplicationUser>(user), oldPassword, newPassword);;
        }

        public async Task DeleteUser(UserDTO user)
        {
            await userRepository.Delete(_mapper.Map<ApplicationUser>(user));
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return _mapper.Map<UserDTO>(await userRepository.FindByEmailAsync(email));
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return (await userRepository.GetAll()).Select(u => _mapper.Map<UserDTO>(u));
        }

        public async Task<bool> SignIn(string name, string password)
        {
            return await userRepository.SignIn(name, password);  
        }

        public async Task<UserDTO> FindByName(string name)
        {
            return _mapper.Map<UserDTO>(await userRepository.FindByNameAsync(name));
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            return _mapper.Map<UserDTO>(await userRepository.GetUserByIdAsync(userId));
        }

        public async Task<List<string>> GetUserRolesAsync(string userName)
        {
            return await userRepository.GetUserRolesAsync(userName);
        }
        public async Task UpdateUser(UserDTO user)
        {
            await userRepository.UpdateUser(_mapper.Map<ApplicationUser>(user));
        }
        public async Task<IList<string>> GetRolesAsync(UserDTO user)
        {
            var list = await userRepository.GetRolesAsync(_mapper.Map<ApplicationUser>(user));
            return list;
        }

        public async Task RemoveFromRolesAsync(UserDTO user, IEnumerable<string> roles)
        {
            await userRepository.RemoveFromRolesAsync(_mapper.Map<ApplicationUser>(user), roles);
        }

        public async Task AddToRoleAsync(UserDTO user, string role)
        {
            await userRepository.AddToRoleAsync(_mapper.Map<ApplicationUser>(user), role);
        }

    }
}
