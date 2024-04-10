using AnimeStar.Models;
using AutoMapper;
using BLL.Entity;
using BLL.Factories;
using BLL.Factories.Interface;
using BLL.ImgProviders;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimeStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFactoryRep _factory;
        private readonly IAnimeImagePathProvider _animeImagePathProvider;

        public HomeController(ILogger<HomeController> logger, IFactoryRep factory, IAnimeImagePathProvider animeImagePathProvider)
        {
            _logger = logger;
            _factory = factory;
            _animeImagePathProvider = animeImagePathProvider;
        }

        public IActionResult Index()
        {
            List<AnimeDTO> latestAnimes = _factory.CreateAnimeRepository().GetAll()
                                                    .OrderByDescending(a => a.ReleaseDate)
                                                    .Take(5)
                                                    .Select(a =>
                                                    {
                                                        a.ImgPath = _animeImagePathProvider.GetAnimeImagePath(a.PictureName);
                                                        return a;
                                                    })
                                                    .ToList();
                                                    
            ViewBag.LatestAnime = latestAnimes;

            return View();
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