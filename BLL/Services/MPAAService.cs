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
    public class MPAAService : IMPAAService
    {
        private readonly IMPAARepository _mpaaRepository;
        private readonly IMapper _mapper;

        public MPAAService(IMPAARepository mpaaRepository, IMapper mapper)
        {
            _mpaaRepository = mpaaRepository;
            _mapper = mapper;
        }

        public void Create(MPAA_DTO entity)
        {
            _mpaaRepository.Create(_mapper.Map<MPAA>(entity));
        }

        public void Delete(int id)
        {
            _mpaaRepository.Delete(id);
        }

        public IEnumerable<MPAA_DTO> Find(Func<MPAA_DTO, bool> predicate)
        {
            Func<MPAA, bool> mpaaPredicate = mpaa => predicate(_mapper.Map<MPAA_DTO>(mpaa));
            var mpaaList = _mpaaRepository.Find(mpaaPredicate);
            return mpaaList.Select(m => _mapper.Map<MPAA_DTO>(m));
        }

        public MPAA_DTO Get(int id)
        {
            return _mapper.Map<MPAA_DTO>(_mpaaRepository.Get(id));
        }

        public IEnumerable<MPAA_DTO> GetAll()
        {
            return _mpaaRepository.GetAll().Select(m => _mapper.Map<MPAA_DTO>(m));
        }

        public void Update(MPAA_DTO entity)
        {
            _mpaaRepository.Update(_mapper.Map<MPAA>(entity));
        }
    }
}
