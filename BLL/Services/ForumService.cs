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
    public class ForumService : IForumService
    {

        private readonly IForumRepository _forumRepository;
        private readonly IMapper _mapper;

        public ForumService(IForumRepository forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public void Create(ForumDTO entity)
        {
            _forumRepository.Create(_mapper.Map<Forum>(entity));
        }

        public void Delete(int id)
        {
            _forumRepository.Delete(id);
        }

        public IEnumerable<ForumDTO> Find(Func<ForumDTO, bool> predicate)
        {
            Func<Forum, bool> forumPredicate = forum => predicate(_mapper.Map<ForumDTO>(forum));
            var forums = _forumRepository.Find(forumPredicate);
            return forums.Select(f => _mapper.Map<ForumDTO>(f));
        }

        public ForumDTO Get(int id)
        {
            return _mapper.Map<ForumDTO>(_forumRepository.Get(id));
        }

        public IEnumerable<ForumDTO> GetAll()
        {
            return _forumRepository.GetAll().Select(f => _mapper.Map<ForumDTO>(f));
        }

        public void Update(ForumDTO entity)
        {
            _forumRepository.Update(_mapper.Map<Forum>(entity));
        }
    }
}
