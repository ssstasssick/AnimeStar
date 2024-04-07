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
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public void Create(CommentDTO entity)
        {
            _commentRepository.Create(_mapper.Map<Comment>(entity));
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
    }
}
