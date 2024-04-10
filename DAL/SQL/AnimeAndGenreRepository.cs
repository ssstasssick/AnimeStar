using DAL.Entity;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQL
{
    public class AnimeAndGenreRepository : IAnimeAndGenreRepository
    {
        public ApplicationDbContext _context;
        public AnimeAndGenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(int animeId, int Id)
        {
            var animeAndGenre = new AnimeAndGenre
            {
                AnimeId = animeId,
                GenreId = Id
            };
            _context.Add(animeAndGenre);
            _context.SaveChanges();

        }

        public void Delete(int animeId, int Id)
        {
            var animeAndGenre = _context.AnimeAndGenres.FirstOrDefault(ag => ag.AnimeId == animeId && ag.GenreId == Id);
            if (animeAndGenre != null)
            {
                _context.AnimeAndGenres.Remove(animeAndGenre);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Genre> Find(int animeId)
        {
            return _context.AnimeAndGenres
                .Where(ag => ag.AnimeId == animeId)
                .Select(ac => ac.Genre)
                .ToList();
        }

        public IEnumerable<Anime> FindAnime(int Id)
        {
            return _context.AnimeAndGenres
                .Where(ac => ac.GenreId == Id)
                .Select(ac => ac.Anime)
                .ToList();
        }
    }
}
