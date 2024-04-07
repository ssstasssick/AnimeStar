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
    public class PersonalListMap : Profile
    {
        public PersonalListMap() 
        {
            CreateMap<PersonalList, PersonalListDTO>()
                    .ForMember(dest => dest.Anime, opt => opt.MapFrom(scr => scr.Anime))
                    .ForMember(dest => dest.User, opt => opt.MapFrom(scr => scr.User))
                    .ReverseMap();
        }
    }
}
