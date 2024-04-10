using DAL.Entity;
using DAL.ImgOutput.Interface;
using DAL.ImgOutput.wwwroot;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class ConfigurationDAL
    {
        public static void ConfigurateDALService(this IServiceCollection services, string connString, string connRoot)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(connString);
            });

            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IAnimeImagePathProvider>(provider =>
            {
                return new FileAnimeImagePathProvider(connRoot);
            });

            services.AddScoped<IAnimeAndCharacterRepository, AnimeAndCharacterRepository>();
            services.AddScoped<IAnimeAndGenreRepository, AnimeAndGenreRepository>();
            services.AddScoped<IAnimeAndStudioRepository, AnimeAndStudioRepository>();
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMPAARepository, MPAARepository>();
            services.AddScoped<IPersonalListRepository, PersonalListRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IStudioRepository, StudioRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
