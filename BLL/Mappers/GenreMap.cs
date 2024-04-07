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
    public class GenreMap : Profile
    {
        public GenreMap() 
        {
            CreateMap<Genre, GenreDTO>()
                    .ForMember(dest => dest.Animes, opt => opt.MapFrom(scr => scr.AnimeAndGenres.Select(aas => aas.Anime))).ReverseMap();
        }   
    }
}
