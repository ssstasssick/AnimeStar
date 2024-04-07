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
    public class StudioRepository : IStudioRepository
    {
        private readonly ApplicationDbContext _context;
        public StudioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Studio entity)
        {
            _context.Studios.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var studio = _context.Studios.Find(id);
            if (studio != null)
            {
                _context.Studios.Remove(studio);
            }
        }

        public IEnumerable<Studio> Find(Func<Studio, bool> predicate)
        {
            return _context.Studios.Where(predicate);
        }

        public Studio Get(int id)
        {
            return _context.Studios.Find(id);
        }

        public IEnumerable<Studio> GetAll()
        {
            return _context.Studios.ToList();
        }

        public void Update(Studio entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
