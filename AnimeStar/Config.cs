using AutoMapper;
using BLL;
using BLL.Factories;
using BLL.Factories.Interface;
using BLL.ImgProviders;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using DAL.ImgOutput.wwwroot;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeStar
{
    public static class Config
    {
        public static void Configurate(this IServiceCollection services, string connString, string connRoot)
        {
            services.ConfigureBLLServices(connString, connRoot);
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); 
                                                     
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IFactoryRep>(new SQLRepFactory(mapper, connString));

        }
    }
}
