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
    public class MPAARepository : IMPAARepository
    {
        private readonly ApplicationDbContext _context;
        public MPAARepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(MPAA entity)
        {
            _context.MPAAs.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var mpaa = _context.MPAAs.Find(id);
            if (mpaa != null)
            {
                _context.Remove(mpaa);
            }
        }

        public IEnumerable<MPAA> Find(Func<MPAA, bool> predicate)
        {
            return _context.MPAAs.Where(predicate);
        }

        public MPAA Get(int id)
        {
            return _context.MPAAs.Find(id);
        }

        public IEnumerable<MPAA> GetAll()
        {
            return _context.MPAAs.ToList();
        }

        public void Update(MPAA entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
