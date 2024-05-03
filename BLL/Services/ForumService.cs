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
        private readonly IAnimeRepository _animeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ForumService(IForumRepository forumRepository, IAnimeRepository animeRepository, ICommentRepository commentRepository
            , IMapper mapper, IUserRepository userRepository)
        {
            _forumRepository = forumRepository;
            _animeRepository = animeRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
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

        public async Task<ForumDTO> LoadPageInf(ForumDTO forum)
        {
            forum.Anime = _mapper.Map<AnimeDTO>(_animeRepository.Get(forum.AnimeId));
            forum.Comments = _commentRepository
                .Find(c => c.ForumId == forum.Id)
                .Select(c => _mapper.Map<CommentDTO>(c))
                .ToList();
            foreach (var comment in forum.Comments)
            {
                // Получаем пользователя асинхронно по его идентификатору
                comment.User = _mapper.Map<UserDTO>(await _userRepository.GetUserByIdAsync(comment.UserId));
                comment.UserName = comment.User.UserName;
            }
            return forum;
        }

        public void Update(ForumDTO entity)
        {
            _forumRepository.Update(_mapper.Map<Forum>(entity));
        }

        public async Task<IEnumerable<ForumDTO>> GetBestAsync(int count)
        {
            var forums = _forumRepository.GetAll().Select(f => _mapper.Map<ForumDTO>(f));

            // Используем async/await для загрузки информации о странице для каждого форума
            foreach (var forum in forums)
            {
                await LoadPageInf(forum);
            }

            // Сортируем форумы по количеству комментариев и дате создания
            var orderedForums = forums
                .Where(f => f.Comments != null)
                .OrderByDescending(f => f.Comments.Count)
                .ThenBy(f => f.CreationDate)
                .Take(count);

            return orderedForums;
        }
    }
}
