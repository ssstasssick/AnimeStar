using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
using DAL.Entity;
using DAL.Interfaces;
using DAL.SQL;
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

        public async Task CreateUser(UserDTO user, string password)
        {
            await userRepository.Create(_mapper.Map<ApplicationUser>(user), password);
        }

        public async Task ChangePassword(UserDTO user, string newPassword)
        {
            await userRepository.ChangePassword(_mapper.Map<ApplicationUser>(user), newPassword);;
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
    }
}
