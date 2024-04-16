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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeStar
{
    public static class Config
    {
        public static void Configurate(this IServiceCollection services, string connString, string connRoot)
        {
            services.ConfigureBLLServices(connString, connRoot);
            services.AddHttpContextAccessor();
            //services.AddScoped<AccountController>();
            //services.AddScoped<HomeController>();
        }
    }
}
