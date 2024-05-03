using AutoMapper;
using BLL.Entity;
using BLL.ImgProviders;
using BLL.Interfaces;
using DAL.Entity;
using DAL.ImgOutput.Interface;
using DAL.ImgOutput.wwwroot;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMPAARepository _mpaaRepository;
        private readonly IAnimeAndCharacterRepository _animeAndCharacterService;
        private readonly IAnimeAndGenreRepository _animeAndGenreRepository;
        private readonly IAnimeAndStudioRepository _animeAndStudioRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IPersonalListRepository _personalListRepository;
        private readonly IForumRepository _forumRepository;
        private readonly IMapper _mapper;

        public AnimeService(IAnimeRepository animeRepository, IMapper mapper, IAnimeAndCharacterRepository animeAndCharacterRepository,
            IAnimeAndGenreRepository animeAndGenreRepository, IAnimeAndStudioRepository animeAndStudioRepository, IMPAARepository mPAARepository,
            ICommentRepository commentRepository, IUserRepository userRepository, IReviewRepository reviewRepository,
            IPersonalListRepository personalListRepository, IForumRepository forumRepository)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
            _animeAndCharacterService = animeAndCharacterRepository;
            _animeAndGenreRepository = animeAndGenreRepository;
            _animeAndStudioRepository = animeAndStudioRepository;
            _mpaaRepository = mPAARepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _personalListRepository = personalListRepository;
            _forumRepository = forumRepository;
        }

        public void Create(AnimeDTO entity)
        {
            _animeRepository.Create(_mapper.Map<Anime>(entity));
        }

        public void Delete(int id)
        {
            _animeRepository.Delete(id);
        }

        public IEnumerable<AnimeDTO> Find(Func<AnimeDTO, bool> predicate)
        {
            Func<Anime, bool> mappedPredicate = anime => predicate(_mapper.Map<AnimeDTO>(anime));
            var anime = _animeRepository.Find(mappedPredicate);
            return anime.Select(anime => _mapper.Map<AnimeDTO>(anime));
        }

        public AnimeDTO Get(int id)
        {
            return _mapper.Map<AnimeDTO>(_animeRepository.Get(id));
        }

        public IEnumerable<AnimeDTO> GetAll()
        {
            return _animeRepository.GetAll().Select(a => _mapper.Map<AnimeDTO>(a));
        }

        public void Update(AnimeDTO entity)
        {
            var animeEntity = _mapper.Map<Anime>(entity);
            _animeRepository.Update(animeEntity);
        }

        public IEnumerable<AnimeDTO> GetBest(int animeCount)
        {
            var reviews = _reviewRepository.GetAll();

            // Получить все аниме
            var allAnime = _animeRepository.GetAll();

            // Создать словарь для хранения средних рейтингов для каждого аниме
            var averageRatings = new Dictionary<int, double>();

            // Рассчитать средний рейтинг для каждого аниме
            foreach (var anime in allAnime)
            {
                // Получить отзывы для текущего аниме
                var animeReviews = reviews.Where(r => r.AnimeId == anime.Id).Select(r => _mapper.Map<ReviewDTO>(r)).ToList();

                // Рассчитать средний рейтинг для текущего аниме
                var averageRating = CalculateAverageRating(animeReviews);
                // Сохранить средний рейтинг в словаре
                averageRatings.Add(anime.Id, averageRating);
            }

            // Установить средние рейтинги для каждого аниме
            foreach (var anime in allAnime)
            {
                anime.AverageRating = averageRatings[anime.Id];
            }

            // Отсортировать аниме по среднему рейтингу и взять указанное количество
            var topAnime = allAnime
                .OrderByDescending(anime => anime.AverageRating)
                .Take(animeCount)
                .Select(a => _mapper.Map<AnimeDTO>(a));


            // Вернуть результат
            return topAnime;
        }

        public IEnumerable<AnimeDTO> GetLatest(int animeCount)
        {
            return GetAll()
                .OrderByDescending(anime => anime.ReleaseDate)
                .Take(animeCount)
                .ToList();
        }

        public AnimeDTO ConnectImg(ImgProviders.IAnimeImagePathProvider animeImagePathProvider, AnimeDTO anime)
        {

            anime.ImgPath = animeImagePathProvider.GetAnimeImagePath(anime.PictureName);
            anime.Characters = anime.Characters.Select(c =>
            {
                c.ImgName = animeImagePathProvider.GetCharacterImagePath(c.ImgName);
                return c;
            }).ToList();
            anime.Studios = anime.Studios.Select(s =>
            {
                s.Description = animeImagePathProvider.GetStudioImagePath(s.Description);
                return s;
            }).ToList();
            return anime;

        }

        public void UpdateAverageRating(int animeId)
        {
            var reviews = _reviewRepository.Find(r => r.AnimeId == animeId).Select(r => _mapper.Map<ReviewDTO>(r)).ToList();

            // Получить все аниме
            var anime = _animeRepository.Get(animeId);

            anime.AverageRating = CalculateAverageRating(reviews);

            _animeRepository.Update(anime);

        }



        public async Task<AnimeDTO> LoadPageInf(AnimeDTO anime)
        {
            anime.Characters = _animeAndCharacterService.Find(anime.Id).Select(c => _mapper.Map<CharacterDTO>(c)).ToList();
            anime.Studios = _animeAndStudioRepository.Find(anime.Id).Select(s => _mapper.Map<StudioDTO>(s)).ToList();
            anime.MPAA = _mpaaRepository.Find(a => a.Id == anime.MPAAId).Select(m => _mapper.Map<MPAA_DTO>(m)).First();
            anime.Genres = _animeAndGenreRepository.Find(anime.Id).Select(g => _mapper.Map<GenreDTO>(g)).ToList();
            anime.Comments = _commentRepository.Find(c => c.AnimeId == anime.Id).Select(c => _mapper.Map<CommentDTO>(c)).ToList();
            anime.Forums = _forumRepository.Find(f => f.AnimeId == anime.Id).Select(f => _mapper.Map<ForumDTO>(f)).ToList();
            // Перебираем комментарии и асинхронно получаем пользователя для каждого комментария
            foreach (var comment in anime.Comments)
            {
                // Получаем пользователя асинхронно по его идентификатору
                comment.User = _mapper.Map<UserDTO>(await _userRepository.GetUserByIdAsync(comment.UserId));
                comment.UserName = comment.User.UserName;
            }

            anime.Reviews = _reviewRepository.Find(r => r.AnimeId == anime.Id)
                .Select(r => _mapper.Map<ReviewDTO>(r))
                .ToList();

            foreach (var review in anime.Reviews)
            {
                review.UserName = _mapper.Map<UserDTO>(await _userRepository.GetUserByIdAsync(review.UserId)).UserName;
            }

            foreach (var forum in anime.Forums)
            {
                forum.UserName = _mapper.Map<UserDTO>(await _userRepository.GetUserByIdAsync(forum.UserId)).UserName;
            }

            anime.PersonalLists = _personalListRepository.Find(l => l.AnimeId == anime.Id).Select(l => _mapper.Map<PersonalListDTO>(l)).ToList();

            return anime;
        }

        public double CalculateAverageRating(List<ReviewDTO> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return 0; // Если нет отзывов, средний рейтинг равен 0
            }

            // Вычисляем общую сумму оценок
            double totalRating = 0;
            foreach (var review in reviews)
            {
                totalRating += (double)review.Rating;
            }

            // Вычисляем средний рейтинг
            double averageRating = totalRating / reviews.Count;
            return averageRating;
        }

        public int CreateWhithId(AnimeDTO entity)
        {
            return _animeRepository.CreateWhithId(_mapper.Map<Anime>(entity));
        }

        public void UpdateAnimeGenres(AnimeDTO anime, List<int> genreIds)
        {
            // Получить текущие связи аниме с жанрами
            var currentGenres = _animeAndGenreRepository.Find(anime.Id);

            // Удалить существующие связи, которых нет в списке выбранных жанров
            foreach (var currentGenre in currentGenres)
            {
                if (!genreIds.Contains(currentGenre.Id))
                {
                    _animeAndGenreRepository.Delete(anime.Id, currentGenre.Id);
                }
            }

            // Добавить новые связи, которых нет в текущих связях
            foreach (var genreId in genreIds)
            {
                if (!currentGenres.Any(c => c.Id == genreId))
                {
                    _animeAndGenreRepository.Create(anime.Id, genreId);
                }
            }
        }

        public void UpdateAnimeCharacters(AnimeDTO anime, List<int> characterIds)
        {
            // Получить текущие связи аниме с персонажами
            var currentCharacters = _animeAndCharacterService.Find(anime.Id);

            // Удалить существующие связи, которых нет в списке выбранных персонажей
            foreach (var currentCharacter in currentCharacters)
            {
                if (!characterIds.Contains(currentCharacter.Id))
                {
                    _animeAndCharacterService.Delete(anime.Id, currentCharacter.Id);
                }
            }

            // Добавить новые связи, которых нет в текущих связях
            foreach (var characterId in characterIds)
            {
                if (!currentCharacters.Any(c => c.Id == characterId))
                {
                    _animeAndCharacterService.Create(anime.Id, characterId);
                }
            }
        }

        public void UpdateAnimeStudios(AnimeDTO anime, int studioId)
        {
            // Получить текущую связь аниме со студией
            var currentStudio = _animeAndStudioRepository.Find(anime.Id).FirstOrDefault();

            // Если текущая связь существует и не совпадает с новой студией, удалить ее
            if (currentStudio != null && currentStudio.Id != studioId)
            {
                _animeAndStudioRepository.Delete(anime.Id, currentStudio.Id);
            }

            // Если новая студия не совпадает с текущей, добавить новую связь
            if (currentStudio == null || currentStudio.Id != studioId)
            {
                _animeAndStudioRepository.Create(anime.Id, studioId);
            }

        }

        public AnimeStatisticsDTO GetAnimeStatistics(int animeId)
        {
            var plannedCount = _personalListRepository.Find(pl => pl.State.Equals("Запланировано")).Where(pl => pl.AnimeId == animeId).Count();
            var watchingCount = _personalListRepository.Find(pl => pl.State.Equals("Смотрю")).Where(pl => pl.AnimeId == animeId).Count();
            var watchedCount = _personalListRepository.Find(pl => pl.State.Equals("Просмотрено")).Where(pl => pl.AnimeId == animeId).Count();
            var postponedCount = _personalListRepository.Find(pl => pl.State.Equals("Отложено")).Where(pl => pl.AnimeId == animeId).Count();
            var droppedCount = _personalListRepository.Find(pl => pl.State.Equals("Брошено")).Where(pl => pl.AnimeId == animeId).Count();

            var statistics = new AnimeStatisticsDTO
            {
                PlannedCount = plannedCount,
                WatchingCount = watchingCount,
                WatchedCount = watchedCount,
                PostponedCount = postponedCount,
                DroppedCount = droppedCount
            };

            return statistics;
        }
    }
}
