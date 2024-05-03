using AnimeStar.Models;
using BLL.Entity;
using BLL.ImgProviders;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeStar.Controllers
{
    public class StudioController : Controller
    {
        private readonly IStudioService _studioService;
        private readonly IAnimeImagePathProvider _animeImagePathProvider;

        public StudioController(IStudioService studioService, IAnimeImagePathProvider animeImagePathProvider)
        {
            _studioService = studioService;
            _animeImagePathProvider = animeImagePathProvider;
        }

        // GET: Studio/Create
        public IActionResult Create()
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                return View();
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // POST: Studio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudioViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    // Обработка загрузки изображения
                    string imageName = null;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageName = _animeImagePathProvider.SaveStudioImg(model.ImageFile);
                    }

                    // Создание объекта StudioDTO с данными из модели
                    var studioDTO = new StudioDTO
                    {
                        Name = model.Name,
                        Description = imageName,
                    };

                    // Сохранение студии
                    _studioService.Create(studioDTO);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Если модель недействительна, вернуть представление с ошибкой
                    return View(model);
                }
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // GET: Studio/Index
        public IActionResult Index()
        {
            var studios = _studioService.GetAll().Select(s => new StudioViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                ImageFile = null // Необходимо присвоить значение изображению
            });
            return View(studios);
        }

        // GET: Studio/Delete/5
        public IActionResult Delete(int? id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var studioDto = _studioService.Get(id.Value);
                if (studioDto == null)
                {
                    return NotFound();
                }

                var studioViewModel = new StudioViewModel
                {
                    Id = studioDto.Id,
                    Name = studioDto.Name,
                    Description = studioDto.Description,
                    ImageFile = null // Необходимо присвоить значение изображению
                };

                return View(studioViewModel);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // POST: Studio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                _studioService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // GET: Studio/Edit/5
        public IActionResult Edit(int? id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var studioDto = _studioService.Get(id.Value);
                if (studioDto == null)
                {
                    return NotFound();
                }

                var studioViewModel = new StudioViewModel
                {
                    Id = studioDto.Id,
                    Name = studioDto.Name,
                    Description = studioDto.Description,
                    ImageFile = null // Необходимо присвоить значение изображению
                };

                return View(studioViewModel);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // POST: Studio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, StudioViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    // Получаем существующую студию для редактирования
                    var existingStudio = _studioService.Get(id);
                    if (existingStudio == null)
                    {
                        return NotFound();
                    }

                    // Обработка загрузки нового изображения, если оно было выбрано
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        existingStudio.Description = _animeImagePathProvider.SaveStudioImg(model.ImageFile);
                    }

                    // Обновляем данные студии
                    existingStudio.Name = model.Name;
                    // Обновление других свойств...

                    // Сохраняем обновленную студию
                    _studioService.Update(existingStudio);

                    return RedirectToAction(nameof(Index));
                }

                // Если модель недействительна, вернуть представление с ошибкой
                return View(model);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }
    }
}
