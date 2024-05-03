using AnimeStar.Models;
using BLL.Entity;
using BLL.ImgProviders;
using BLL.Interfaces;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AnimeStar.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly IAnimeImagePathProvider _animeImagePathProvider;

        public CharacterController(ICharacterService characterService, IAnimeImagePathProvider animeImagePathProvider)
        {
            _characterService = characterService;
            _animeImagePathProvider = animeImagePathProvider;
        }

        // GET: Character
        public IActionResult Index()
        {
            var characters = _characterService.GetAll();
            return View(characters);
        }

        // GET: Character/Create
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

        // POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CharacterViewModel model)
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
                        imageName = _animeImagePathProvider.SaveCharacterImg(model.ImageFile);
                    }

                    // Создание объекта CharacterDTO с данными из модели
                    var characterDTO = new CharacterDTO
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ImgName = imageName, // Здесь сохраняем имя изображения
                                             // Другие свойства...
                    };

                    // Сохранение персонажа
                    _characterService.Create(characterDTO);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Обработка случая, когда пользователь не является администратором
                    return Redirect("/Account/Authorization");
                }
            }

            // Если модель недействительна, вернуть представление с ошибкой
            return View(model);
        }

        // GET: Character/Edit/5
        public IActionResult Edit(int? id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var characterDto = _characterService.Get(id.Value);
                if (characterDto == null)
                {
                    return NotFound();
                }
                var characterViewModel = new CharacterViewModel
                {
                    Id = characterDto.Id,
                    Name = characterDto.Name,
                    Description = characterDto.Description,
                    ImageFile = null,
                };
                return View(characterViewModel);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // POST: Character/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CharacterViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    // Получаем существующего персонажа для редактирования
                    var existingCharacter = _characterService.Get(id);
                    if (existingCharacter == null)
                    {
                        return NotFound();
                    }

                    // Обработка загрузки нового изображения, если оно было выбрано
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        existingCharacter.ImgName = _animeImagePathProvider.SaveCharacterImg(model.ImageFile);
                    }

                    // Обновляем данные персонажа
                    existingCharacter.Name = model.Name;
                    existingCharacter.Description = model.Description;
                    // Обновление других свойств...

                    // Сохраняем обновленного персонажа
                    _characterService.Update(existingCharacter);

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

        // GET: Character/Delete/5
        public IActionResult Delete(int? id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var characterDto = _characterService.Get(id.Value);
                if (characterDto == null)
                {
                    return NotFound();
                }

                return View(characterDto);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("moder", StringComparison.OrdinalIgnoreCase))
            {
                _characterService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }
    }
}




