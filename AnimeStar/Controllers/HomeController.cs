using AnimeStar.Models;
using AutoMapper;
using BLL.Entity;
using BLL.Factories;
using BLL.Factories.Interface;
using BLL.ImgProviders;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace AnimeStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimeImagePathProvider _animeImagePathProvider;
        private readonly IAnimeService _animeService;
        private readonly IForumService _forumService;
        private readonly UserManager<UserDTO> _userManager;
        private readonly IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger, IFactoryRep factory, IAnimeImagePathProvider animeImagePathProvider, UserManager<UserDTO> userManager,
            IForumService forumService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _animeImagePathProvider = animeImagePathProvider;
            _animeService = factory.CreateAnimeRepository();
            _userManager = userManager;
            _forumService = forumService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {


            IEnumerable<AnimeDTO> latestAnime = _animeService.GetLatest(6).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
            IEnumerable<AnimeDTO> bestAnime = _animeService.GetBest(9).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
            IEnumerable<ForumDTO> bestForum = await _forumService.GetBestAsync(5);

            ViewBag.LatestAnime = latestAnime;

            ViewBag.BestAnime = bestAnime;

            ViewBag.BestForums = bestForum;

            return View();
        }

        public async Task<IActionResult> AnimeDetails(int id)
        {
            AnimeDTO anime = _animeService.Get(id);
            if (anime == null)
            {
                return NotFound();
            }
            anime = await _animeService.LoadPageInf(anime);
            anime = _animeService.ConnectImg(_animeImagePathProvider, anime);

            var statistics = _animeService.GetAnimeStatistics(anime.Id);

            ViewBag.Statistics = statistics;

            _memoryCache.Set("AnimeDTO", anime);

            return View("Views/Details/AnimeDetails.cshtml", anime);
        }

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return Json(new List<AnimeDTO>());
            }

            // Выполните поиск аниме по введенному термину
            var searchResults = _animeService.Find(anime => anime.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            return Json(searchResults);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}