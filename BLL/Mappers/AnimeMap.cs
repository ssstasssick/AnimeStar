using BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entity;

namespace BLL.Mappers
{
    public class AnimeMap : Profile
    {
        public AnimeMap()
        {
            CreateMap<Anime, AnimeDTO>()
                .ForMember(dest => dest.Characters, opt => opt.MapFrom(scr => scr.AnimeAndCharacters.Select(aac => aac.Character)))
                .ForMember(dest => dest.Studios, opt => opt.MapFrom(scr => scr.AnimeAndStudios.Select(aas => aas.Studio)))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(scr => scr.AnimeAndGenres.Select(aag => aag.Genre)))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(scr => scr.Reviews))
                .ForMember(dest => dest.MPAA, opt => opt.MapFrom(scr => scr.MPAA))
                .ForMember(dest => dest.Forums, opt => opt.MapFrom(scr => scr.Forums))
                .ForMember(dest => dest.PersonalLists, opt => opt.MapFrom(scr => scr.PersonalLists))
                .ReverseMap();
        }
    }
}
