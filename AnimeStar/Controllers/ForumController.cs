using BLL.Entity;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeStar.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IAnimeService _animeService;
        public ForumController(IForumService forumService) 
        {
            _forumService = forumService;
        }

        public async Task<IActionResult> ForumDetails(int id)
        {
            ForumDTO forum = _forumService.Get(id);
            if (forum == null)
            {
                return NotFound();
            }
            forum = await _forumService.LoadPageInf(forum);
            return View("Views/Details/ForumDetails.cshtml", forum);
        }
    }
}
