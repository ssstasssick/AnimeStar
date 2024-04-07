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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        
        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public void Create(ReviewDTO entity)
        {
            _reviewRepository.Create(_mapper.Map<Review>(entity));
        }

        public void Delete(int id)
        {
            _reviewRepository.Delete(id);
        }

        public IEnumerable<ReviewDTO> Find(Func<ReviewDTO, bool> predicate)
        {
            Func<Review, bool> reviewPredicate = review => predicate(_mapper.Map<ReviewDTO>(review));
            var reviews = _reviewRepository.Find(reviewPredicate);
            return reviews.Select(r => _mapper.Map<ReviewDTO>(r));
        }

        public ReviewDTO Get(int id)
        {
            return _mapper.Map<ReviewDTO>(_reviewRepository.Get(id));
        }

        public IEnumerable<ReviewDTO> GetAll()
        {
            return _reviewRepository.GetAll().Select(r => _mapper.Map<ReviewDTO>(r));
        }

        public void Update(ReviewDTO entity)
        {
            _reviewRepository.Update(_mapper.Map<Review>(entity));
        }
    }
}
