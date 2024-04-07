using BLL;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;

namespace AnimeStar
{
    public static class Config
    {
        public static void Configurate(this IServiceCollection services, string connString)
        {
            services.ConfigureBLLServices(connString);
            services.AddAutoMapper(typeof(MappingProfile));

        }
    }
}
