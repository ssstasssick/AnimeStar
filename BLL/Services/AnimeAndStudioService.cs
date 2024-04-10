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
    public class AnimeAndStudioService : IAnimeAndStudioService
    {
        private readonly IAnimeAndStudioRepository _animeAndStudioRepository;
        private readonly IMapper _mapper;

        public AnimeAndStudioService(IAnimeAndStudioRepository animeAndStudioRepository, IMapper mapper)
        {
            _animeAndStudioRepository = animeAndStudioRepository;
            _mapper = mapper;
        }
        public void Create(int animeId, int Id)
        {
            _animeAndStudioRepository.Create(animeId, Id);
        }

        public void Delete(int animeId, int Id)
        {
            _animeAndStudioRepository.Delete(animeId, Id);
        }

        public IEnumerable<StudioDTO> Find(int animeId)
        {
            return _animeAndStudioRepository.Find(animeId).Select(aas => _mapper.Map<StudioDTO>(aas));
        }

        public IEnumerable<AnimeDTO> FindAnime(int Id)
        {
            return _animeAndStudioRepository.FindAnime(Id).Select(aas => _mapper.Map<AnimeDTO>(aas));
        }
    }
}
