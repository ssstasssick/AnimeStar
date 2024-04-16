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

        public IEnumerable<AnimeDTO> ConnectImg(IAnimeImagePathProvider animeImagePathProvider, IEnumerable<AnimeDTO> animeList)
        {
            return animeList
                 .OrderByDescending(a => a.ReleaseDate)
                 .Take(5)
                 .Select(a =>
                 {
                    a.ImgPath = animeImagePathProvider.GetAnimeImagePath(a.PictureName);
                    return a;
                 });
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

    }
}
