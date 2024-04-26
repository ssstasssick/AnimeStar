using AnimeStar.Controllers;
using AutoMapper;
using BLL;
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

            services.AddAuthorization();
            services.AddHttpContextAccessor();

        }

    }
}
