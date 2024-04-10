using DAL.Entity;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQL
{
    public class AnimeAndCharacterRepository : IAnimeAndCharacterRepository
    {
        public ApplicationDbContext _context;
        public AnimeAndCharacterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(int animeId, int characterId)
        {
            var animeAndCharacter = new AnimeAndCharacter
            {
                AnimeId = animeId,
                CharacterId = characterId
            };
            _context.AnimeAndCharacters.Add(animeAndCharacter);
            _context.SaveChanges();
        }

        public void Delete(int animeId, int characterId)
        {
            var animeAndCharacter = _context.AnimeAndCharacters.FirstOrDefault(ac => ac.AnimeId == animeId && ac.CharacterId == characterId);
            if (animeAndCharacter != null)
            {
                _context.AnimeAndCharacters.Remove(animeAndCharacter);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Anime> FindAnime(int characterId)
        {
            return _context.AnimeAndCharacters
                .Where(ac => ac.CharacterId == characterId)
                .Select(ac => ac.Anime)
                .ToList();
        }

        public IEnumerable<Character> Find(int animeId)
        {
            return _context.AnimeAndCharacters
                .Where(ac => ac.AnimeId == animeId)
                .Select(ac => ac.Character)
                .ToList();
        }
    }
}
