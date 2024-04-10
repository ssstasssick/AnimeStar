using DAL.Entity;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQL
{
    public class AnimeAndStudioRepository : IAnimeAndStudioRepository
    {
        public ApplicationDbContext _context;
        public AnimeAndStudioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(int animeId, int Id)
        {
            var animeAndStudio = new AnimeAndStudio
            {
                AnimeId = animeId,
                StudioId = Id
            };
            _context.Add(animeAndStudio);
            _context.SaveChanges();
        }

        public void Delete(int animeId, int Id)
        {
            var animeAndStudio = _context.AnimeAndStudios.FirstOrDefault(ag => ag.AnimeId == animeId && ag.StudioId == Id);
            if (animeAndStudio != null)
            {
                _context.AnimeAndStudios.Remove(animeAndStudio);
            }
        }

        public IEnumerable<Studio> Find(int animeId)
        {
            return _context.AnimeAndStudios
                .Where(ag => ag.AnimeId == animeId)
                .Select(ac => ac.Studio)
                .ToList();
        }

        public IEnumerable<Anime> FindAnime(int Id)
        {
            return _context.AnimeAndStudios
                .Where(ac => ac.StudioId == Id)
                .Select(ac => ac.Anime)
                .ToList();
        }
    }
}
