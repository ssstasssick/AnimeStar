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
using DAL.Entity;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStar.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(IFactoryRep factory, RoleManager<IdentityRole> roleManager)
        {
            _userService = factory.CreateUserService();
            _roleManager = roleManager;
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
                    await _userService.CreateUser(userDTO, model.Password, "default");
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Неверные данные для регистрации: ";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибка валидации данных для регистрации:";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ViewBag.ErrorMessage += " " + error.ErrorMessage;
                    }
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
                        var roles = await _userService.GetUserRolesAsync(user.UserName.ToString());
                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, model.UserName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id),
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim("RegisterDate", user.RegisterDate.ToString()),
                                new Claim("Roles", string.Join(", ", roles))
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
                    // Отображение сообщения об ошибке на странице
                    ViewBag.ErrorMessage = "Неверное имя пользователя или пароль.";
                }
            }

            // Если дошли сюда, значит аутентификация не удалась, возвращаем представление снова
            return View(model);
        }


        public async Task<IActionResult> UserInfo()
        {
            var currentUser = HttpContext.User.Identity.Name;
            // Получите другую информацию о пользователе из вашей системы аутентификации и заполните модель представления
            var userInfoViewModel = new UserInfoViewModel
            {
                UserName = currentUser,
                Email = User.FindFirst(ClaimTypes.Email).Value.ToString(),
                RegisterDate = User.FindFirst("RegisterDate").Value.ToString(),
                Roles = User.FindFirst("Roles").Value.ToString()

            };
            return View(userInfoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Users()
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                var users = _userService.GetAllUsers().Result.ToList();
                return View(users);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }

        }

        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var user = new UserDTO { UserName = model.UserName, Email = model.Email, RegisterDate = DateTime.Now, Role = model.Role };
                        await _userService.CreateUser(user, model.Password, user.Role);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("UserName", "Пользователь с таким именем уже существует.");
                        var roles = _roleManager.Roles.ToList();
                        ViewBag.Roles = roles;
                        return View(model);
                    }


                }
                return RedirectToAction("Users");
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        var user = await _userService.GetUserByIdAsync(id);
                        var roles = await _userService.GetUserRolesAsync(user.UserName.ToString());
                        user.Role = string.Join(", ", roles);
                        ViewBag.User = user;
                        return View("UserDetails");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
                return RedirectToAction("Users");
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(); // Отобразить страницу 404 Not Found, если пользователь не найден
                }

                var roles = _roleManager.Roles.ToList();
                ViewBag.Roles = roles;

                // Подготавливаем модель для представления с текущими значениями пользователя
                var model = new EditUserViewModel
                {
                    Id = user.Id,
                    CurrentUserName = user.UserName,
                    // Не устанавливаем текущий email, так как мы не позволяем изменять его
                };

                return View(model); // Отобразить представление для редактирования пользователя
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }

        // Метод действия POST для обновления пользователя
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.GetUserByIdAsync(model.Id);
                    if (user == null)
                    {
                        return NotFound(); // Show 404 Not Found page if user not found
                    }

                    // Update username if provided
                    if (!string.IsNullOrEmpty(model.NewUserName))
                    {
                        user.UserName = model.NewUserName;
                    }

                    // Update password if provided
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        if (string.IsNullOrEmpty(model.OldPassword))
                        {
                            ModelState.AddModelError("OldPassword", "Старый пароль обязателен для изменения пароля.");
                            return View(model);
                        }

                        await _userService.ChangePassword(user, model.OldPassword, model.NewPassword);
                    }

                    if (!string.IsNullOrEmpty(model.NewRole))
                    {
                        // Remove user from all roles
                        var userRoles = await _userService.GetRolesAsync(user);
                        await _userService.RemoveFromRolesAsync(user, userRoles);

                        // Add user to new role
                        await _userService.AddToRoleAsync(user, model.NewRole);
                    }

                    await _userService.UpdateUser(user); // Update user in the database

                    return RedirectToAction("Users"); // Redirect to user list page after update
                }

                // If the model is invalid, re-render the edit view with validation errors
                return View(model);
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }



        // Метод действия для удаления пользователя
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var rolesClaim = User.FindFirst("Roles");
            if (rolesClaim != null && rolesClaim.Value.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(); // Отобразить страницу 404 Not Found, если пользователь не найден
                }

                await _userService.DeleteUser(user); // Удалить пользователя

                return RedirectToAction("Users"); // Перенаправить на страницу со списком пользователей после удаления
            }
            else
            {
                // Обработка случая, когда пользователь не является администратором
                return Redirect("/Account/Authorization");
            }
        }


    }
}
