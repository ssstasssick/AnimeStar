using AutoMapper;
using BLL.Entity;
using BLL.Interfaces;
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
    public class PersonalListService : IPersonalListService
    {
        private readonly IPersonalListRepository _personalListRepository;
        private readonly IMapper _mapper;

        public PersonalListService(IPersonalListRepository personalListRepository, IMapper mapper)
        {
            _personalListRepository = personalListRepository;
            _mapper = mapper;
        }

        public void Create(PersonalListDTO entity)
        {
            _personalListRepository.Create(_mapper.Map<PersonalList>(entity));
        }

        public void Delete(int id)
        {
            _personalListRepository.Delete(id);
        }

        public IEnumerable<PersonalListDTO> Find(Func<PersonalListDTO, bool> predicate)
        {
            Func<PersonalList, bool> personalListPredicate = personalList => predicate(_mapper.Map<PersonalListDTO>(personalList));
            var personalLists = _personalListRepository.Find(personalListPredicate);
            return personalLists.Select(p => _mapper.Map<PersonalListDTO>(p));
        }

        public PersonalListDTO Get(int id)
        {
            return _mapper.Map<PersonalListDTO>(_personalListRepository.Get(id));
        }

        public IEnumerable<PersonalListDTO> GetAll()
        {
            return _personalListRepository.GetAll().Select(p => _mapper.Map<PersonalListDTO>(p));
        }

        public void Update(PersonalListDTO entity)
        {
            _personalListRepository.Update(_mapper.Map<PersonalList>(entity));
        }
    }
}
