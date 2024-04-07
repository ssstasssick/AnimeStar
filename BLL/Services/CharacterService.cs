using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.Entity;
using DAL.Interfaces;
using DAL.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharacterService(CharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public void Create(CharacterDTO entity)
        {
            _characterRepository.Create(_mapper.Map<Character>(entity));
        }

        public void Delete(int id)
        {
            _characterRepository.Delete(id);
        }

        public IEnumerable<CharacterDTO> Find(Func<CharacterDTO, bool> predicate)
        {
            Func<Character, bool> characterPredicate = character => predicate(_mapper.Map<CharacterDTO>(character));
            var characters = _characterRepository.Find(characterPredicate);
            return characters.Select(c => _mapper.Map<CharacterDTO>(c));
        }

        public CharacterDTO Get(int id)
        {
            return _mapper.Map<CharacterDTO>(_characterRepository.Get(id));
        }

        public IEnumerable<CharacterDTO> GetAll()
        {
            return _characterRepository.GetAll().Select(c => _mapper.Map<CharacterDTO>(c));
        }

        public void Update(CharacterDTO entity)
        {
            _characterRepository.Update(_mapper.Map<Character>(entity));
        }
    }
}
