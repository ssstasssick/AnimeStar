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
using Microsoft.EntityFrameworkCore;

namespace BLL.Factories
{
    public class SQLRepFactory : IFactoryRep
    {
        private IMapper _mapper;
        ApplicationDbContext context;

        public SQLRepFactory(IMapper mapper, string connString)
        {
            _mapper = mapper;
            context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connString)
                .Options);
        }

        public IAnimeAndCharacterService CreateAnimeAndCharacterService()
        {
            return new AnimeAndCharacterService(new AnimeAndCharacterRepository(context), _mapper);
        }

        public IAnimeAndGenreService CreateAnimeAndGenreService()
        {
            return new AnimeAndGenreService(new AnimeAndGenreRepository(context), _mapper);
        }

        public IAnimeAndStudioService CreateAnimeAndStudioService()
        {
            return new AnimeAndStudioService(new AnimeAndStudioRepository(context), _mapper);
        }

        public IAnimeService CreateAnimeRepository()
        {
            return new AnimeService(new AnimeRepository(context), _mapper);
        }

        public ICharacterService CreateCharacterRepository()
        {
            return new CharacterService(new CharacterRepository(context), _mapper);
        }

        public ICommentService CreateCommentRepository()
        {
            return new CommentService(new  CommentRepository(context), _mapper);
        }

        public IForumService CreateForumRepository()
        {
            return new ForumService(new  ForumRepository(context), _mapper);
        }

        public IGenreService CreateGenreRepository()
        {
            return new GenreService(new GenreRepository(context), _mapper);
        }

        public IMPAAService CreateMPAARepository()
        {
            return new MPAAService(new MPAARepository(context), _mapper);
        }

        public IPersonalListService CreatePersonalListRepository()
        {
            return new PersonalListService(new PersonalListRepository(context), _mapper);
        }

        public IReviewService CreateReviewRepository()
        {
            return new ReviewService(new  ReviewRepository(context), _mapper);
        }

        public IStudioService CreateStudioRepository()
        {
            return new StudioService(new  StudioRepository(context), _mapper);
        }

        public IUserService CreateUserService()
        {
            return new UserService(new UserRepository(context), _mapper);
        }
    }
}
