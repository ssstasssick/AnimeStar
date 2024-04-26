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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Review entity)
        {
            _context.Reviews.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var review = _context.Reviews.Find(id);
        }

        public IEnumerable<Review> Find(Func<Review, bool> predicate)
        {
            return _context.Reviews.Where(predicate);
        }

        public Review Get(int id)
        {
            return _context.Reviews.Find(id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public void Update(Review entity)
        {
            var existingEntity = _context.Reviews.Find(entity.Id);
            if (existingEntity != null)
            {
                // Копируем значения из новой сущности в существующую
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
