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
    public class CharacterRepository : ICharacterRepository
    {

        private readonly ApplicationDbContext _context;
        public CharacterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Character entity)
        {
            _context.Characters.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var character = _context.Characters.Find(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Character> Find(Func<Character, bool> predicate)
        {
            return _context.Characters.Where(predicate);
        }

        public Character Get(int id)
        {
            return _context.Characters.Find(id);
        }

        public IEnumerable<Character> GetAll()
        {
            return _context.Characters.ToList();
        }

        public void Update(Character entity)
        {
            var existingCharacter = _context.Characters.Find(entity.Id);
            if (existingCharacter != null)
            {
                existingCharacter.Name = entity.Name;
                existingCharacter.Description = entity.Description;
                existingCharacter.ImgName = entity.ImgName;
                _context.Characters.Update(existingCharacter);
                _context.SaveChanges();
            }
        }
    }
}
