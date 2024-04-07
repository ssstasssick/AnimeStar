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
    public class CommentMap : Profile
    {
        public CommentMap() 
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(scr => scr.User))
                .ForMember(dest => dest.Forum, opt => opt.MapFrom(scr => scr.Forum))
                .ReverseMap();
        }
    }
}
