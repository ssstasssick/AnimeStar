using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
using DAL.Entity;
using DAL.Interfaces;
using DAL.SQL; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;

        public AnimeService(IAnimeRepository animeRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
        }

        public void Create(AnimeDTO entity)
        {
            _animeRepository.Create(_mapper.Map<Anime>(entity));
        }

        public void Delete(int id)
        {
            _animeRepository.Delete(id);
        }

        public IEnumerable<AnimeDTO> Find(Func<AnimeDTO, bool> predicate)
        {
            Func<Anime, bool> mappedPredicate = anime => predicate(_mapper.Map<AnimeDTO>(anime));
            var anime = _animeRepository.Find(mappedPredicate);
            return anime.Select(anime => _mapper.Map<AnimeDTO>(anime)); 
        }

        public AnimeDTO Get(int id)
        {
            return _mapper.Map<AnimeDTO>(_animeRepository.Get(id));
        }

        public IEnumerable<AnimeDTO> GetAll()
        {
            return _animeRepository.GetAll().Select(a => _mapper.Map<AnimeDTO>(a));
        }

        public void Update(AnimeDTO entity)
        {
            _animeRepository.Update(_mapper.Map<Anime>(entity));
        }
    }
}
