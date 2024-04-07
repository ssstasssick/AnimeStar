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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void Create(UserDTO entity)
        {
            _userRepository.Create(_mapper.Map<User>(entity));
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<UserDTO> Find(Func<UserDTO, bool> predicate)
        {
            Func<User, bool> userPredicate = user => predicate(_mapper.Map<UserDTO>(user));
            var users = _userRepository.Find(userPredicate);
            return users.Select(u => _mapper.Map<UserDTO>(u));
        }

        public UserDTO Get(int id)
        {
            return _mapper.Map<UserDTO>(_userRepository.Get(id));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return _userRepository.GetAll().Select(u => _mapper.Map<UserDTO>(u));
        }

        public void Update(UserDTO entity)
        {
            _userRepository.Update(_mapper.Map<User>(entity));
        }
    }
}
