using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AnimeAndGenreService : IAnimeAndGenreService
    {
        private readonly IAnimeAndGenreRepository _animeAndGenreRepository;
        private readonly IMapper _mapper;

        public AnimeAndGenreService(IAnimeAndGenreRepository animeAndGenreRepository, IMapper mapper)
        {
            _animeAndGenreRepository = animeAndGenreRepository;
            _mapper = mapper;
        }

        public void Create(int animeId, int Id)
        {
            _animeAndGenreRepository.Create(animeId, Id);
        }

        public void Delete(int animeId, int Id)
        {
            _animeAndGenreRepository.Delete(animeId, Id);
        }

        public IEnumerable<GenreDTO> Find(int animeId)
        {
            return _animeAndGenreRepository.Find(animeId).Select(aag => _mapper.Map<GenreDTO>(aag));
        }

        public IEnumerable<AnimeDTO> FindAnime(int Id)
        {
            return _animeAndGenreRepository.FindAnime(Id).Select(aag => _mapper.Map<AnimeDTO>(aag));
        }
    }
}
