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
    public class ForumMap : Profile
    {
        public ForumMap() 
        {
            CreateMap<Forum, ForumDTO>()
                    .ForMember(dest => dest.UserDTO, opt => opt.MapFrom(scr => scr.User))
                    .ForMember(dest => dest.Anime, opt => opt.MapFrom(scr => scr.Anime))
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(scr => scr.Comments))
                    .ReverseMap();
        }
    }
}
