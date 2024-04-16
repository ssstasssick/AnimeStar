using AutoMapper;
using BLL.Entity;
using BLL.Factories;
using BLL.Factories.Interface;
using BLL.ImgProviders;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using DAL;
using DAL.Entity;
using DAL.ImgOutput.wwwroot;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class ConfigurationBLL
    {
        public static void ConfigureBLLServices(this IServiceCollection services, string connString, string connRoot)
        {
            services.ConfigurateDALService(connString, connRoot);

            services.AddScoped<IAnimeImagePathProvider>(provider =>
            {
                return new ImgProvider(connRoot);
            });

            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMPAAService, MPAAService>();
            services.AddScoped<IPersonalListService, PersonalListService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IStudioService, StudioService>();
            services.AddScoped<IAnimeAndCharacterService, AnimeAndCharacterService>();
            services.AddScoped<IAnimeAndStudioService, AnimeAndStudioService>();
            services.AddScoped<IAnimeAndGenreService, AnimeAndGenreService>();
            services.AddScoped<IUserService, UserService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());

            });

            services.AddScoped<IMapper>(provider =>
            {
                return mapperConfig.CreateMapper();

            });

            services.AddIdentityCore<UserDTO>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IFactoryRep, SQLRepFactory>();



            


        }
    }
}
