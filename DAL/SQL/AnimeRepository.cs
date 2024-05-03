using DAL.Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQL
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly ApplicationDbContext _context;
        public AnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CreateWhithId(Anime entity)
        {
            _context.Animes.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Create(Anime entity)
        {
            _context.Animes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var anime = _context.Animes.Find(id);
            if (anime != null)
            {
                _context.Remove(anime);
                _context.SaveChanges();
            }

        }

        public IEnumerable<Anime> Find(Func<Anime, bool> predicate)
        {
            return _context.Animes.Where(predicate);
        }

        public Anime Get(int id)
        {
            return _context.Animes.Find(id);
        }

        public IEnumerable<Anime> GetAll()
        {
            return _context.Animes
                .Include(a => a.AnimeAndCharacters)
                .ThenInclude(aac => aac.Character)
                .ToList();
        }

        public void Update(Anime entity)
        {
            var existingAnime = _context.Animes.Find(entity.Id);
            if (existingAnime == null)
            {
                throw new Exception($"Anime with id {entity.Id} not found");
            }

            existingAnime.AnimeState = entity.AnimeState;
            existingAnime.Title = entity.Title;
            existingAnime.Description = entity.Description;
            existingAnime.LenghtOfTheFilm = entity.LenghtOfTheFilm;
            existingAnime.NumberOfEpisodes = entity.NumberOfEpisodes;
            existingAnime.MPAAId = entity.MPAAId;
            existingAnime.PictureName = entity.PictureName;
            existingAnime.ReleaseDate = entity.ReleaseDate;
            existingAnime.TypeOfAnime = entity.TypeOfAnime;           

            // Сохраняем изменения в контексте данных
            _context.SaveChanges();
        }
    }
}
