using AnimeStar.Models;
using BLL.Entity;
using BLL.ImgProviders;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace AnimeStar.Controllers
{
    public class AnimeController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IAnimeService _animeService;
        private readonly IUserService _userService;
        private readonly ICharacterService _characterService;
        private readonly IReviewService _reviewService;
        private readonly IForumService _forumService;
        private readonly IAnimeImagePathProvider _animeImagePathProvider;
        private readonly IMPAAService _mpaaService;
        private readonly IGenreService _genreService;
        private readonly IStudioService _studioService;
        private readonly IAnimeAndStudioService _animeAndStudioService;
        private readonly IAnimeAndCharacterService _animeAndCharacterService;
        private readonly IAnimeAndGenreService _animeAndGenreService;
        private readonly IMemoryCache _memoryCache;


        public AnimeController(ICommentService commentService, IAnimeService animeService, IUserService userService,
            ICharacterService characterService, IReviewService reviewService, IForumService forumService, IAnimeImagePathProvider animeImagePathProvider,
            IMPAAService mPAAService, IStudioService studioService, IGenreService genreService, IAnimeAndCharacterService animeAndCharacterService,
            IAnimeAndGenreService animeAndGenreService, IAnimeAndStudioService animeAndStudioService, IMemoryCache memoryCache)
        {
            _commentService = commentService;
            _animeService = animeService;
            _userService = userService;
            _characterService = characterService;
            _reviewService = reviewService;
            _forumService = forumService;
            _animeImagePathProvider = animeImagePathProvider;
            _mpaaService = mPAAService;
            _studioService = studioService;
            _genreService = genreService;
            _animeAndCharacterService = animeAndCharacterService;
            _animeAndGenreService = animeAndGenreService;
            _animeAndStudioService = animeAndStudioService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDTO comment, int? forumId = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "Чтобы добавить комментарий, пожалуйста, сначала войдите в систему или зарегистрируйтесь.";
            }
            else
            {
                comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.UserName = User.Identity.Name;
                comment.CreationDate = DateTime.Now;
                comment.ForumId = forumId;
                await _commentService.Create(comment);
                // Возвращаем частичное представление с новым комментарием
                return PartialView("_CommentPartial", comment);
            }
            // Если пользователь не аутентифицирован, просто возвращаем пустой результат
            return new EmptyResult();
        }

        public async Task<IActionResult> CharacterDetails(CharacterDTO character)
        {
            if (character == null)
            {
                return NotFound();
            }

            return View("Views/Details/CharacterDetails.cshtml", character);
        }

        public async Task<IActionResult> CharacterDetailsById(int characterId)
        {
            var character = _characterService.Get(characterId);
            character.ImgName = _animeImagePathProvider.GetCharacterImagePath(character.ImgName);
            if (character == null)
            {
                return NotFound();
            }

            return View("Views/Details/CharacterDetails.cshtml", character);
        }

        [HttpPost]
        public ActionResult Rate(int animeId, int rating)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = User.Identity.Name;

                // Проверяем, существует ли оценка от этого пользователя для данного аниме
                var existingReview = _reviewService.Find(r => r.UserId == userId && r.AnimeId == animeId).FirstOrDefault();

                if (existingReview == null)
                {
                    // Если оценки от пользователя нет, создаем новую оценку
                    _reviewService.Create(new ReviewDTO { AnimeId = animeId, Rating = rating, UserId = userId });
                }
                else
                {
                    existingReview.UserName = userName;
                    existingReview.Rating = rating;
                    _reviewService.Update(existingReview);
                }
                return Json(new { success = true });
            }
            else
            {
                // Если пользователь не аутентифицирован, возвращаем сообщение о том, что для оценки нужно войти или зарегистрироваться
                return Json(new { success = false, message = "Для оценки аниме пожалуйста, войдите в систему или зарегистрируйтесь." });
            }
        }

        [HttpPost]
        public ActionResult AddReview(int animeId, string text)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // Проверяем, существует ли оценка от этого пользователя для данного аниме
                var existingReview = _reviewService.Find(r => r.UserId == userId && r.AnimeId == animeId).FirstOrDefault();
                if (existingReview != null)
                {
                    existingReview.Text = text;
                    _reviewService.Update(existingReview);
                }
                existingReview.UserName = User.Identity.Name;
                return PartialView("_ReviewPartial", existingReview);
            }
            else
            {
                // Если пользователь не аутентифицирован, возвращаем сообщение о том, что для оценки нужно войти или зарегистрироваться
                return Json(new { success = false, message = "Для того, чтобы оставить рецензию аниме пожалуйста, войдите в систему или зарегистрируйтесь." });
            }
        }

        [HttpPost]
        public IActionResult AddForum(ForumDTO forum)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "Чтобы создать форум, пожалуйста, сначала войдите в систему или зарегистрируйтесь.";
            }
            else
            {
                forum.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                forum.UserName = User.Identity.Name;
                forum.CreationDate = DateTime.Now;
                _forumService.Create(forum);

                // После создания форума возвращаем частичное представление с обновленным списком форумов
                var forums = _forumService.Find(f => f.AnimeId == forum.AnimeId).ToList(); // Получаем обновленный список форумов
                return PartialView("_ForumListPartial", forums);
            }
            // Если пользователь не аутентифицирован, просто возвращаем пустой результат
            return new EmptyResult();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllAnime(string type)
        {
            int animeCount = _animeService.GetAll().Count();
            IEnumerable<AnimeDTO> anime = null;
            switch (type)
            {
                case "best":
                    anime = _animeService.GetBest(animeCount).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                case "latest":
                    anime = _animeService.GetLatest(animeCount).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                case "all":
                    anime = _animeService.GetAll().Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                case "currentYear":
                    anime = _animeService.GetBest(animeCount).Where(a => a.ReleaseDate.Year == DateTime.Now.Year).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                case "lastYear":
                    anime = _animeService.GetBest(animeCount).Where(a => a.ReleaseDate.Year - 1 == DateTime.Now.Year - 1).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                case "completed":
                    anime = _animeService.GetBest(animeCount).Where(a => a.AnimeState.ToUpper() == "Вышло".ToUpper()).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                case "uncompleted":
                    anime = _animeService.GetBest(animeCount).Where(a => a.AnimeState.ToUpper() == "Онгоинг".ToUpper()).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
                default:
                    anime = _animeService.GetAll().Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                    break;
            }
            ViewBag.anime = anime;
            return View("Views/Anime/Index.cshtml");
        }

        [HttpGet]
        public IActionResult AddAnime()
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                // Подготовка данных для выпадающего списка MPAA
                ViewBag.MpaaRating = new SelectList(_mpaaService.GetAll().ToList(), "Id", "Name");

                // Подготовка данных для выпадающего списка жанров
                ViewBag.GenreList = _genreService.GetAll();

                // Подготовка данных для выпадающего списка персонажей
                var characterList = _characterService.GetAll(); // Пример
                ViewBag.CharacterList = characterList;

                // Подготовка данных для выпадающего списка студий
                ViewBag.StudioList = _studioService.GetAll();

                return View();
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpPost]
        public IActionResult AddAnime(AnimeViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    var existingAnime = _animeService.Find(a => a.Title == model.Title);
                    if (existingAnime != null)
                    {
                        ModelState.AddModelError("Title", "Аниме с таким названием уже существует.");
                        // Подготовка данных для выпадающего списка MPAA рейтингов
                        ViewBag.MpaaRating = new SelectList(_mpaaService.GetAll().ToList(), "Id", "Name");
                        // Подготовка данных для выпадающего списка жанров
                        ViewBag.GenreList = _genreService.GetAll();
                        // Подготовка данных для выпадающего списка персонажей
                        ViewBag.CharacterList = _characterService.GetAll();
                        // Подготовка данных для выпадающего списка студий
                        ViewBag.StudioList = _studioService.GetAll();
                        return View(model);
                    }
                    // Создание объекта Anime из модели представления
                    var anime = new AnimeDTO
                    {
                        Title = model.Title,
                        Description = model.Description,
                        ReleaseDate = model.ReleaseDate,
                        PictureName = model.ImageFile != null ? _animeImagePathProvider.SaveAnimeImg(model.ImageFile) : "none",
                        TypeOfAnime = model.TypeOfAnime,
                        NumberOfEpisodes = model.NumberOfEpisodes,
                        LenghtOfTheFilm = model.LenghtOfTheFilm,
                        AnimeState = model.AnimeState,
                        MPAAId = model.MpaaId,
                    };

                    // Добавление аниме в базу данных
                    int animeId = _animeService.CreateWhithId(anime);
                    if (model.GenreIds != null)
                    {
                        foreach (var genreId in model.GenreIds)
                        {
                            _animeAndGenreService.Create(animeId, genreId);
                        }
                    }
                    if (model.CharacterIds != null)
                    {
                        foreach (var characterId in model.CharacterIds)
                        {
                            _animeAndCharacterService.Create(animeId, characterId);
                        }
                    }
                    if (model.StudioId != null)
                        _animeAndStudioService.Create(animeId, (int)model.StudioId);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.MpaaRating = new SelectList(_mpaaService.GetAll().ToList(), "Id", "Name");

                // Подготовка данных для выпадающего списка жанров
                ViewBag.GenreList = _genreService.GetAll();

                // Подготовка данных для выпадающего списка персонажей
                ViewBag.CharacterList = _characterService.GetAll();

                // Подготовка данных для выпадающего списка студий
                ViewBag.StudioList = _studioService.GetAll();
                return View();
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpGet]
        public IActionResult EditAnime()
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                AnimeDTO anime;
                if (!_memoryCache.TryGetValue("AnimeDTO", out anime))
                {
                    return NotFound();
                }
                var model = new AnimeViewModel
                {
                    // Заполните свойства модели представления значениями из объекта anime
                    Id = anime.Id,
                    Title = anime.Title,
                    Description = anime.Description,
                    LenghtOfTheFilm = anime.LenghtOfTheFilm,
                    CharacterIds = anime.Characters.Select(c => c.Id).ToList(),
                    GenreIds = anime.Genres.Select(c => c.Id).ToList(),
                    StudioId = anime.Studios.FirstOrDefault()?.Id,
                    MpaaId = anime.MPAAId,
                    ReleaseDate = anime.ReleaseDate,
                    TypeOfAnime = anime.TypeOfAnime,
                    NumberOfEpisodes = anime.NumberOfEpisodes,
                    AnimeState = anime.AnimeState,
                };
                // Подготовка данных для выпадающего списка MPAA
                ViewBag.MpaaRating = new SelectList(_mpaaService.GetAll().ToList(), "Id", "Name");

                // Подготовка данных для выпадающего списка жанров
                ViewBag.GenreList = _genreService.GetAll();

                // Подготовка данных для выпадающего списка персонажей
                var characterList = _characterService.GetAll(); // Пример
                ViewBag.CharacterList = characterList;

                // Подготовка данных для выпадающего списка студий
                ViewBag.StudioList = _studioService.GetAll();

                return View(model);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        public IActionResult EditAnime(AnimeViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    // Получите существующий объект AnimeDTO из базы данных
                    var existingAnime = _animeService.Get(model.Id);
                    if (existingAnime == null)
                    {
                        return NotFound(); // Обработка ситуации, когда аниме с указанным id не найдено
                    }

                    // Обновите свойства аниме на основе данных из модели представления
                    existingAnime.Title = model.Title;
                    existingAnime.Description = model.Description;
                    existingAnime.ReleaseDate = model.ReleaseDate;
                    if (model.ImageFile != null)
                    {
                        existingAnime.PictureName = _animeImagePathProvider.SaveAnimeImg(model.ImageFile);

                    }
                    existingAnime.TypeOfAnime = model.TypeOfAnime;
                    existingAnime.NumberOfEpisodes = model.NumberOfEpisodes;
                    existingAnime.LenghtOfTheFilm = model.LenghtOfTheFilm;
                    existingAnime.AnimeState = model.AnimeState;
                    existingAnime.MPAAId = model.MpaaId;

                    // Обновите связи аниме с жанрами
                    var selectedGenres = model.GenreIds ?? new List<int>();
                    _animeService.UpdateAnimeGenres(existingAnime, selectedGenres);

                    // Обновите связи аниме с персонажами
                    var selectedCharacters = model.CharacterIds ?? new List<int>();
                    _animeService.UpdateAnimeCharacters(existingAnime, selectedCharacters);

                    if (model.StudioId.HasValue)
                    {
                        _animeService.UpdateAnimeStudios(existingAnime, model.StudioId.Value);
                    }
                    // Сохраните обновленный объект аниме в базе данных
                    _animeService.Update(existingAnime);

                    // Обработка успешного изменения аниме
                    return RedirectToAction("AnimeDetails", "Home", new { id = existingAnime.Id });
                }



                // Обработка невалидных данных модели
                ViewBag.MpaaRating = new SelectList(_mpaaService.GetAll().ToList(), "Id", "Name");
                ViewBag.GenreList = _genreService.GetAll();
                ViewBag.CharacterList = _characterService.GetAll();
                ViewBag.StudioList = _studioService.GetAll();
                return View(model);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }
        [HttpPost]
        public IActionResult DeleteAnime(int id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                // Получите существующий объект AnimeDTO из базы данных
                var existingAnime = _animeService.Get(id);
                if (existingAnime == null)
                {
                    return NotFound(); // Обработка ситуации, когда аниме с указанным id не найдено
                }

                // Удалите аниме из базы данных
                _animeService.Delete(id);

                // Перенаправьте пользователя на страницу списка аниме или другую страницу
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpPost]
        public IActionResult DeleteComment(int commentId)
        {

            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                _commentService.Delete(commentId);
                return Ok(); // Возвращаем успешный результат
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpPost]
        public IActionResult DeleteForum(int forumId)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                _forumService.Delete(forumId);
                return Ok(); // Возвращаем успешный результат
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpPost]
        public IActionResult DeleteTextReview(int reviewId)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                var review = _reviewService.Get(reviewId);
                review.Text = null;
                _reviewService.Update(review);
                return Ok(); // Возвращаем успешный результат
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

    }
}
