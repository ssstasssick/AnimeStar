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
using Microsoft.AspNetCore.Identity;
using DAL.Entity;
using DAL.Interfaces;

namespace BLL.Factories
{
    public class SQLRepFactory : IFactoryRep
    {
        IAnimeAndCharacterService animeAndCharacterService;
        IAnimeAndGenreService animeAndGenreService;
        IAnimeAndStudioService animeAndStudioService;
        IAnimeService animeService;
        ICharacterService characterService;
        ICommentService commentService;
        IForumService forumService;
        IGenreService genreService;
        IMPAAService mPAAService;
        IPersonalListService personalListService;
        IReviewService reviewService;
        IStudioService studioService;
        IUserService userService;

        public SQLRepFactory(IAnimeAndCharacterService animeAndCharacterService, IAnimeAndGenreService animeAndGenreService, 
            IAnimeAndStudioService animeAndStudioService, IAnimeService animeService, ICharacterService characterService, ICommentService commentService, 
            IForumService forumService, IGenreService genreService, IMPAAService mPAAService, IPersonalListService personalListService,
            IReviewService reviewService, IStudioService studioService, IUserService userService)
        {
            this.userService = userService;
            this.characterService = characterService;
            this.commentService = commentService;
            this.forumService = forumService;
            this.genreService = genreService;
            this.animeService = animeService;
            this.animeAndCharacterService = animeAndCharacterService;
            this.animeAndGenreService = animeAndGenreService;
            this.animeAndStudioService = animeAndStudioService;
            this.mPAAService = mPAAService;
            this.personalListService = personalListService;
            this.reviewService = reviewService;
            this.studioService = studioService;

        }

        public IAnimeAndCharacterService CreateAnimeAndCharacterService()
        {
            return animeAndCharacterService;
        }

        public IAnimeAndGenreService CreateAnimeAndGenreService()
        {
            return animeAndGenreService;
        }

        public IAnimeAndStudioService CreateAnimeAndStudioService()
        {
            return animeAndStudioService;
        }

        public IAnimeService CreateAnimeRepository()
        {
            return animeService;
        }

        public ICharacterService CreateCharacterRepository()
        {
            return characterService;
        }

        public ICommentService CreateCommentRepository()
        {
            return commentService;
        }

        public IForumService CreateForumRepository()
        {
            return forumService;
        }

        public IGenreService CreateGenreRepository()
        {
            return genreService;
        }

        public IMPAAService CreateMPAARepository()
        {
            return mPAAService;
        }

        public IPersonalListService CreatePersonalListRepository()
        {
            return personalListService;
        }

        public IReviewService CreateReviewRepository()
        {
            return reviewService;
        }

        public IStudioService CreateStudioRepository()
        {
            return studioService;
        }

        public IUserService CreateUserService()
        {
            return userService;
        }
    }
}
