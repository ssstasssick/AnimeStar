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
    public class GenreService : IGenreService
    {
        
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public void Create(GenreDTO entity)
        {
            _genreRepository.Create(_mapper.Map<Genre>(entity));
        }

        public void Delete(int id)
        {
            _genreRepository.Delete(id);
        }

        public IEnumerable<GenreDTO> Find(Func<GenreDTO, bool> predicate)
        {
            Func<Genre, bool> genrePredicate = genre => predicate(_mapper.Map<GenreDTO>(genre));
            var genres = _genreRepository.Find(genrePredicate);
            return genres.Select(g => _mapper.Map<GenreDTO>(g));   
        }

        public GenreDTO Get(int id)
        {
            return _mapper.Map<GenreDTO>(_genreRepository.Get(id));
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            return _genreRepository.GetAll().Select(g => _mapper.Map<GenreDTO>(g));
        }

        public void Update(GenreDTO entity)
        {
            _genreRepository.Update(_mapper.Map<Genre>(entity));
        }
    }
}
