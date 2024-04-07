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
               _context.Remove(id);
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
            return _context.Animes.ToList();
        }

        public void Update(Anime entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
