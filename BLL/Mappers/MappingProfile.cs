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
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Character, CharacterDTO>()
                .ForMember(dest => dest.Animes, opt => opt
                .MapFrom(scr => scr.AnimeAndCharacters
                .Select(aac => aac.Anime)))
                .ReverseMap();

            CreateMap<Anime, AnimeDTO>()
                .ForMember(dest => dest.Characters, opt => opt.MapFrom(scr => scr.AnimeAndCharacters.Select(aac => aac.Character)))
                .ForMember(dest => dest.Studios, opt => opt.MapFrom(scr => scr.AnimeAndStudios.Select(aas => aas.Studio)))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(scr => scr.AnimeAndGenres.Select(aag => aag.Genre)))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(scr => scr.Reviews))
                .ForMember(dest => dest.MPAA, opt => opt.MapFrom(scr => scr.MPAA))
                .ForMember(dest => dest.Forums, opt => opt.MapFrom(scr => scr.Forums))
                .ForMember(dest => dest.PersonalLists, opt => opt.MapFrom(scr => scr.PersonalLists))
                .ReverseMap();

            CreateMap<Studio, StudioDTO>()
                .ForMember(dest => dest.Animes, opt => opt.MapFrom(scr => scr.AnimeAndStudios.Select(aas => aas.Anime))).ReverseMap();

            CreateMap<Genre, GenreDTO>()
                .ForMember(dest => dest.Animes, opt => opt.MapFrom(scr => scr.AnimeAndGenres.Select(aas => aas.Anime))).ReverseMap();

            CreateMap<Forum, ForumDTO>()
                .ForMember(dest => dest.UserDTO, opt => opt.MapFrom(scr => scr.User))
                .ForMember(dest => dest.Anime, opt => opt.MapFrom(scr => scr.Anime))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(scr => scr.Comments))
                .ReverseMap();

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(scr => scr.User))
                .ForMember(dest => dest.Forum, opt => opt.MapFrom(scr => scr.Forum))
                .ReverseMap();

            CreateMap<MPAA, MPAA_DTO>()
                .ForMember(dest => dest.Animes, opt => opt.MapFrom(scr => scr.Animes))
                .ReverseMap();

            CreateMap<PersonalList, PersonalListDTO>()
                .ForMember(dest => dest.Anime, opt => opt.MapFrom(scr => scr.Anime))
                .ForMember(dest => dest.User, opt => opt.MapFrom(scr => scr.User))
                .ReverseMap();

            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.Anime, opt => opt.MapFrom(scr => scr.Anime))
                .ForMember(dest => dest.User, opt => opt.MapFrom(scr => scr.User))
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(scr => scr.Reviews))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(scr => scr.Comments))
                .ReverseMap();


        }

    }
}
