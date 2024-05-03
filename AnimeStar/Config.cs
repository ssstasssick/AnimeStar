using AnimeStar.Controllers;
using AutoMapper;
using BLL;
using BLL.Entity;
using BLL.Factories;
using BLL.Factories.Interface;
using BLL.ImgProviders;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using DAL.Entity;
using DAL.ImgOutput.wwwroot;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AnimeStar
{
    public static class Config
    {
        public static void Configurate(this IServiceCollection services, string connString, string connRoot)
        {
            services.ConfigureBLLServices(connString, connRoot);
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Authorization";
                    options.AccessDeniedPath = "/Shared/Error";
                })
                 .AddCookie("Identity.Application");


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
                options.AddPolicy("Moder", policy => policy.RequireRole("moder"));
                options.AddPolicy("Default", policy => policy.RequireRole("default"));

            });
            services.AddHttpContextAccessor();

            ConfigureAdminUser(services.BuildServiceProvider());

        }

        private static async Task ConfigureAdminUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserDTO>>();
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                if (await userService.FindByName("Admin") == null)
                {
                    var adminUser = new UserDTO
                    {
                        UserName = "Admin",
                        RegisterDate = DateTime.Now,
                        Email = "admin@gmail.com"
                        
                    };

                    userService.CreateUser(adminUser, "Admin_12345", "admin").Wait();
                }
            }
        }

    }
}
