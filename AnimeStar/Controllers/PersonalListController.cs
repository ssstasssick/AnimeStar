using BLL.Entity;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AnimeStar.Controllers
{
    public class PersonalListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IPersonalListService _personalListService;
        private readonly IAnimeService _animeService;

        public PersonalListController(IPersonalListService personalListService, IAnimeService animeService)
        {
            _personalListService = personalListService;
            _animeService = animeService;
        }

        [HttpPost]
        public IActionResult AddOrUpdatePersonalListItem(string state, int animeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                State stateEnum = (State)Enum.Parse(typeof(State), state);

                var existingPersonalListItem = _personalListService.Find(l => l.UserId == userId && l.AnimeId == animeId).FirstOrDefault();

                if (existingPersonalListItem != null)
                {
                    // Загрузка объекта из контекста данных
                    existingPersonalListItem = _personalListService.Get(existingPersonalListItem.Id);

                    existingPersonalListItem.State = stateEnum;
                    _personalListService.Update(existingPersonalListItem);
                }
                else
                {
                    _personalListService.Create(new PersonalListDTO { State = stateEnum, AnimeId = animeId, UserId = userId });
                }

                return Ok();
            }
            else
            {
                // Если пользователь не аутентифицирован, возвращаем сообщение о том, что для оценки нужно войти или зарегистрироваться
                return Json(new { success = false, message = "Для оценки аниме пожалуйста, войдите в систему или зарегистрируйтесь." });
            }
        }


        [HttpPost]
        public IActionResult GetAnimeByState(string state)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Находим аниме по состоянию для указанного пользователя
            var animeByState = _personalListService.Find(pl => pl.UserId == userId && pl.State.ToString() == state);

            // Проходим по каждому элементу коллекции и устанавливаем значение поля Anime
            var personalListForState = _personalListService
    .Find(pl => pl.UserId == userId && pl.State.ToString() == state)
    .ToList();

            if (personalListForState.Any())
            {
                // Для каждой записи PersonalListDTO получаем связанное аниме
                var animeForState = personalListForState
                    .Select(pl => new PersonalListDTO
                    {
                        Id = pl.Id,
                        AnimeId = pl.AnimeId,
                        Anime = _animeService.Get(pl.AnimeId), // Получаем связанное аниме по его Id
                        UserId = pl.UserId,
                        User = pl.User,
                        State = pl.State
                    })
                    .ToList();

                // Возвращаем аниме в виде частичного представления или JSON
                return PartialView("_AnimeListPartial", animeForState);
            }
            else
            {
                // Если список пустой, возвращаем сообщение о пустом списке
                return PartialView("_EmptyListPartial");
            }

        }

    }
}
