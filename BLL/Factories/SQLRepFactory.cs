using AutoMapper;
using BLL.Factories.Interface;
using BLL.Interfaces;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.SQL;
using DAL;

namespace BLL.Factories
{
    public class SQLRepFactory : IFactoryRep
    {
        private IMapper _mapper;
        private ApplicationDbContext _context = ;

        public SQLRepFactory(IMapper mapper, string connString)
        {
            _mapper = mapper;
        }

        public IAnimeService CreateAnimeRepository()
        {
            return new AnimeService(new AnimeRepository(), _mapper);
        }

        public ICharacterService CreateCharacterRepository()
        {
            return new CharacterService(new CharacterRepository(), _mapper);
        }

        public ICommentService CreateCommentRepository()
        {
            return new CommentService(new  CommentRepository(), _mapper);
        }

        public IForumService CreateForumRepository()
        {
            return new ForumService(new  ForumRepository(), _mapper);
        }

        public IGenreService CreateGenreRepository()
        {
            return new GenreService(new GenreRepository(), _mapper);
        }

        public IMPAAService CreateMPAARepository()
        {
            return new MPAAService(new MPAARepository(), _mapper);
        }

        public IPersonalListService CreatePersonalListRepository()
        {
            return new PersonalListService(new PersonalListRepository(), _mapper);
        }

        public IReviewService CreateReviewRepository()
        {
            return new ReviewService(new  ReviewRepository(), _mapper);
        }

        public IStudioService CreateStudioRepository()
        {
            return new StudioService(new  StudioRepository(), _mapper);
        }

        public IUserService CreateUserService()
        {
            return new UserService(new UserRepository(), _mapper);
        }
    }
}
