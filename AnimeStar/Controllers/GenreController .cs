using AnimeStar.Models;
using BLL.Entity;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeStar.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: Genre
        public IActionResult Index()
        {
            var genres = _genreService.GetAll();
            return View(genres);
        }

        // GET: Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genreDTO = new GenreDTO
                {
                    Name = model.Name,
                    Description = model.Description
                };

                _genreService.Create(genreDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Genre/Delete/5
        public IActionResult Delete(int id)
        {
            var genreDTO = _genreService.Get(id);
            if (genreDTO == null)
            {
                return NotFound();
            }

            return View(genreDTO);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _genreService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
