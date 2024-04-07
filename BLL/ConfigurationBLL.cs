using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using DAL;
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
        public static void ConfigureBLLServices(this IServiceCollection services, string connString)
        {
            services.ConfigurateDALService(connString);

            services.AddScoped<AnimeMap>();
            services.AddScoped<CharacterMap>();
            services.AddScoped<CommentMap>();
            services.AddScoped<ForumMap>();
            services.AddScoped<UserMap>();
            services.AddScoped<GenreMap>();
            services.AddScoped<MPAA_Map>();
            services.AddScoped<PersonalListMap>();
            services.AddScoped<ReviewMap>();
            services.AddScoped<StudioMap>();

            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMPAAService, MPAAService>();
            services.AddScoped<IPersonalListService, PersonalListService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IStudioService, StudioService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
