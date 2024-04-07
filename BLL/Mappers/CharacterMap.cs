using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public class CharacterMap : Profile
    {
        public CharacterMap() 
        {
                 CreateMap<Character, CharacterDTO>()
                .ForMember(dest => dest.Animes, opt => opt
                .MapFrom(scr => scr.AnimeAndCharacters
                .Select(aac => aac.Anime)))
                .ReverseMap();
        }

    }

}
