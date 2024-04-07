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
    public class StudioService : IStudioService
    {
        private readonly IStudioRepository _studioRepository;
        private readonly IMapper _mapper;
        
        public StudioService(IStudioRepository studioRepository, IMapper mapper)
        {
            _studioRepository = studioRepository;
            _mapper = mapper;
        }

        public void Create(StudioDTO entity)
        {
            _studioRepository.Create(_mapper.Map<Studio>(entity));
        }

        public void Delete(int id)
        {
            _studioRepository.Delete(id);
        }

        public IEnumerable<StudioDTO> Find(Func<StudioDTO, bool> predicate)
        {
            Func<Studio, bool> studioPredicate = studio => predicate(_mapper.Map<StudioDTO>(studio));
            var studios = _studioRepository.Find(studioPredicate);
            return studios.Select(s => _mapper.Map<StudioDTO>(s));
        }

        public StudioDTO Get(int id)
        {
            return _mapper.Map<StudioDTO>(_studioRepository.Get(id));
        }

        public IEnumerable<StudioDTO> GetAll()
        {
            return _studioRepository.GetAll().Select(s => _mapper.Map<StudioDTO>(s));
        }

        public void Update(StudioDTO entity)
        {
            _studioRepository.Update(_mapper.Map<Studio>(entity));
        }
    }
}
