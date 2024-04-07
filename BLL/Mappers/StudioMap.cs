using AutoMapper;
using BLL.Entity;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public class StudioMap : Profile
    {
        public StudioMap() 
        {
            CreateMap<Studio, StudioDTO>()
                    .ForMember(dest => dest.Animes, opt => opt.MapFrom(scr => scr.AnimeAndStudios.Select(aas => aas.Anime))).ReverseMap();
        }
    }
}
