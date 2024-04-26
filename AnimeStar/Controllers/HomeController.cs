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
using System.Diagnostics;

namespace AnimeStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimeImagePathProvider _animeImagePathProvider;
        private readonly IAnimeService _animeService;
        private readonly UserManager<UserDTO> _userManager;
        public HomeController(ILogger<HomeController> logger, IFactoryRep factory, IAnimeImagePathProvider animeImagePathProvider, UserManager<UserDTO> userManager)
        {
            _logger = logger;
            _animeImagePathProvider = animeImagePathProvider;
            _animeService = factory.CreateAnimeRepository();
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            

            IEnumerable<AnimeDTO> latestAnime = _animeService.GetLatest(5).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));

            IEnumerable<AnimeDTO> bestAnime = _animeService.GetBest(9).Select(a => _animeService.ConnectImg(_animeImagePathProvider, a));
                                                    
            ViewBag.LatestAnime = latestAnime;

            ViewBag.BestAnime = bestAnime;



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

            return View("Views/Details/AnimeDetails.cshtml", anime);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}