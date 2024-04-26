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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Create(CommentDTO entity)
        {

            var comment = _mapper.Map<Comment>(entity);
            var userTask = _userRepository.GetUserByIdAsync(entity.UserId); // Получаем задачу
            var user = await userTask;
            comment.User = user;
            _commentRepository.Create(comment);
        }

        public void Delete(int id)
        {
            _commentRepository.Delete(id);

        }

        public IEnumerable<CommentDTO> Find(Func<CommentDTO, bool> predicate)
        {
            Func<Comment, bool> commentPredicate = comment => predicate(_mapper.Map<CommentDTO>(comment));
            var comments = _commentRepository.Find(commentPredicate);
            return comments.Select(c => _mapper.Map<CommentDTO>(c));
        }

        public CommentDTO Get(int id)
        {
            return _mapper.Map<CommentDTO>(_commentRepository.Get(id));
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            return _commentRepository.GetAll().Select(c => _mapper.Map<CommentDTO>(c));
        }

        public void Update(CommentDTO entity)
        {
            _commentRepository.Update(_mapper.Map<Comment>(entity));
        }

        public async Task<UserDTO> GetUserAsync(CommentDTO entity)
        {
            var user = await _userRepository.GetUserByIdAsync(_commentRepository.Get(entity.Id).UserId);
            return _mapper.Map<UserDTO>(user);
        }

        void IService<CommentDTO>.Create(CommentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
