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
    public class ForumRepository : IForumRepository
    {
        private readonly ApplicationDbContext _context;
        public ForumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Forum entity)
        {
            _context.Forums.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var forum = _context.Forums.Find(id);
            if (forum != null)
            {
                _context.Forums.Remove(forum);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Forum> Find(Func<Forum, bool> predicate)
        {
            return _context.Forums.Where(predicate);
        }

        public Forum Get(int id)
        {
            return _context.Forums.Find(id);
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.ToList();
        }

        public void Update(Forum entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
