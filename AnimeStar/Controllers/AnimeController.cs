using BLL.Entity;
using BLL.Interfaces;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AnimeController(ICommentService commentService, IAnimeService animeService, IUserService userService,
            ICharacterService characterService, IReviewService reviewService)
        {
            _commentService = commentService;
            _animeService = animeService;
            _userService = userService;
            _characterService = characterService;
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDTO comment)
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
                comment.ForumId = null;
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

        [HttpPost]
        public ActionResult Rate(int animeId, int rating)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Проверяем, существует ли оценка от этого пользователя для данного аниме
                var existingReview = _reviewService.Find(r => r.UserId == userId && r.AnimeId == animeId).FirstOrDefault();

                if (existingReview == null)
                {
                    // Если оценки от пользователя нет, создаем новую оценку
                    _reviewService.Create(new ReviewDTO { AnimeId = animeId, Rating = rating, UserId = userId });
                }
                else
                {
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

        

        public IActionResult Index()
        {
            return View();
        }


    }
}
