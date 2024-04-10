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
    public class AnimeAndCharacterService : IAnimeAndCharacterService
    {
        private readonly IAnimeAndCharacterRepository _animeAndCharacterRepository;
        private readonly IMapper _mapper;

        public AnimeAndCharacterService(IAnimeAndCharacterRepository animeAndCharacterRepository, IMapper mapper)
        {
            _animeAndCharacterRepository = animeAndCharacterRepository;
            _mapper = mapper;
        }

        public void Create(int animeId, int Id)
        {
            _animeAndCharacterRepository.Create(animeId, Id);
        }

        public void Delete(int animeId, int Id)
        {
            _animeAndCharacterRepository.Delete(animeId, Id);
        }

        public IEnumerable<CharacterDTO> Find(int animeId)
        {
            return _animeAndCharacterRepository.Find(animeId).Select(ac => _mapper.Map<CharacterDTO>(ac));
        }

        public IEnumerable<AnimeDTO> FindAnime(int Id)
        {
            return _animeAndCharacterRepository.FindAnime(Id).Select(ac => _mapper.Map<AnimeDTO>(ac));
        }
    }
}
