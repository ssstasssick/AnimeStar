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
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Genre entity)
        {
            _context.Genres.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
            {
                _context.Remove(genre);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Genre> Find(Func<Genre, bool> predicate)
        {
            return _context.Genres.Where(predicate);
        }

        public Genre Get(int id)
        {
            return _context.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genres.ToList();
        }

        public void Update(Genre entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
