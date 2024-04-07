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
    public class MPAA_Map : Profile
    {
        public MPAA_Map()
        {
            CreateMap<MPAA, MPAA_DTO>()
                    .ForMember(dest => dest.Animes, opt => opt.MapFrom(scr => scr.Animes))
                    .ReverseMap();
        }
    }
}
