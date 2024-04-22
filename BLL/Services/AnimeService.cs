using AutoMapper;
using BLL.Entity;
using BLL.ImgProviders;
using BLL.Interfaces;
using DAL.Entity;
using DAL.ImgOutput.wwwroot;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMPAARepository _mpaaRepository;
        private readonly IAnimeAndCharacterRepository _animeAndCharacterService;
        private readonly IAnimeAndGenreRepository _animeAndGenreRepository;
        private readonly IAnimeAndStudioRepository _animeAndStudioRepository;
        private readonly IMapper _mapper;

        public AnimeService(IAnimeRepository animeRepository, IMapper mapper, IAnimeAndCharacterRepository animeAndCharacterRepository, 
            IAnimeAndGenreRepository animeAndGenreRepository, IAnimeAndStudioRepository animeAndStudioRepository, IMPAARepository mPAARepository)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
            _animeAndCharacterService = animeAndCharacterRepository;
            _animeAndGenreRepository = animeAndGenreRepository;
            _animeAndStudioRepository = animeAndStudioRepository;
            _mpaaRepository = mPAARepository;
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

        public IEnumerable<AnimeDTO> GetBest(int animeCount)
        {
            return GetAll()
                .OrderByDescending(anime => anime.AverageRating)
                .Take(animeCount);
        }

        public IEnumerable<AnimeDTO> GetLatest(int animeCount)
        {
            return GetAll()
                .OrderByDescending(anime => anime.ReleaseDate)
                .Take(animeCount)
                .ToList();
        }

        public AnimeDTO ConnectImg(IAnimeImagePathProvider animeImagePathProvider, AnimeDTO anime)
        {

            anime.ImgPath = animeImagePathProvider.GetAnimeImagePath(anime.PictureName);
            return anime;

        }

        public void UpdateAverageRating(int animeId)
        {
            //var anime = _context.Anime
            //    .Include(a => a.Reviews)
            //    .FirstOrDefault(a => a.Id == animeId);

            //if (anime != null)
            //{
            //    if (anime.Reviews.Any())
            //    {
            //        anime.AverageRating = anime.Reviews.Average(r => r.Rating);
            //    }
            //    else
            //    {
            //        anime.AverageRating = 0.0;
            //    }

            //    _context.SaveChanges();
            //}
        }

        public AnimeDTO LoadPageInf(AnimeDTO anime)
        {
            anime.Characters = _animeAndCharacterService.Find(anime.Id).Select(c => _mapper.Map<CharacterDTO>(c)).ToList();
            anime.Studios = _animeAndStudioRepository.Find(anime.Id).Select(s => _mapper.Map<StudioDTO>(s)).ToList();
            anime.MPAA = _mpaaRepository.Find(a => a.Id == anime.MPAAId).Select(m => _mapper.Map<MPAA_DTO>(m)).First();
            anime.Genres = _animeAndGenreRepository.Find(anime.Id).Select(g => _mapper.Map<GenreDTO>(g)).ToList();
            return anime;

        }
    }
}
