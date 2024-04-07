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
    public class UserMap : Profile
    {
        public UserMap() 
        {
            CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Reviews, opt => opt.MapFrom(scr => scr.Reviews))
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(scr => scr.Comments))
                    .ReverseMap();
        }
    }
}
