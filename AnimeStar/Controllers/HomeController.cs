using AnimeStar.Models;
using AutoMapper;
using BLL.Entity;
using BLL.Factories;
using BLL.Factories.Interface;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimeStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICharacterService _characterService;

        public HomeController(ILogger<HomeController> logger, ICharacterService characterService)
        {
            _logger = logger;
            _characterService = characterService;
        }

        public IActionResult Index()
        {
            //factory.CreateCharacterRepository()
            //    .Create(new BLL.Entity.CharacterDTO
            //    {
            //        Name = "First",
            //        Description = "First",
            //    });
            List<CharacterDTO> characters = _characterService.GetAll().ToList();
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