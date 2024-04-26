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
    public class PersonalListRepository : IPersonalListRepository
    {
        private readonly ApplicationDbContext _context;
        public PersonalListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(PersonalList entity)
        {
            _context.PersonalLists.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var personalList = _context.PersonalLists.Find(id);
            if (personalList != null)
            {
                _context.Remove(personalList);
            }
        }

        public IEnumerable<PersonalList> Find(Func<PersonalList, bool> predicate)
        {
            return _context.PersonalLists.Where(predicate);
        }

        public PersonalList Get(int id)
        {
            return _context.PersonalLists.Find(id);
        }

        public IEnumerable<PersonalList> GetAll()
        {
            return _context.PersonalLists.ToList();
        }

        public void Update(PersonalList entity)
        {
            var existingEntity = _context.PersonalLists.Find(entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
