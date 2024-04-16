using AnimeStar.Models;
using BLL.Entity;
using BLL.Factories.Interface;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimeStar.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IFactoryRep factory)
        {
            _userService = factory.CreateUserService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDTO = new UserDTO
                {
                    Email = model.Email,
                    UserName = model.Name
                };

                try
                {
                    await _userService.CreateUser(userDTO, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create user: " + ex.Message);
                }
            }

            return View(model);
        }
    }
}
