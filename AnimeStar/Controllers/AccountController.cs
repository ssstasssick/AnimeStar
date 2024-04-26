using AnimeStar.Models;
using BLL.Entity;
using BLL.Factories.Interface;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDTO = new UserDTO
                {
                    Email = model.Email,
                    UserName = model.Name,
                    RegisterDate = DateTime.Now
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authorization(AuthorizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.SignIn(model.UserName, password: model.Password);

                    if (result)
                    {
                        var user = await _userService.FindByName(model.UserName);

                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("RegisterDate", user.RegisterDate.ToString())
                    // Можно добавить другие идентификационные данные пользователя по необходимости
                };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // Обработка ошибки входа
                    ViewBag.ErrorMessage = "Неверное имя пользователя или пароль";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Неверное имя пользователя или пароль";
            }

            // Если дошли сюда, значит аутентификация не удалась, возвращаем представление снова
            return View(model);
        }

        public IActionResult UserInfo()
        {
            var currentUser = HttpContext.User.Identity.Name;
            // Получите другую информацию о пользователе из вашей системы аутентификации и заполните модель представления
            var userInfoViewModel = new UserInfoViewModel
            {
                UserName = currentUser,
                Email = User.FindFirst(ClaimTypes.Email).Value.ToString(),
                RegisterDate = User.FindFirst("RegisterDate").Value.ToString(),

            };
            return View(userInfoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
