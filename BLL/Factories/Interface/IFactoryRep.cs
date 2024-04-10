using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Factories.Interface
{
    public interface IFactoryRep
    {
        IAnimeService CreateAnimeRepository();
        ICharacterService CreateCharacterRepository();
        ICommentService CreateCommentRepository();
        IPersonalListService CreatePersonalListRepository();
        IForumService CreateForumRepository();
        IGenreService CreateGenreRepository();
        IMPAAService CreateMPAARepository();
        IReviewService CreateReviewRepository();
        IStudioService CreateStudioRepository();
        IUserService CreateUserService();
        IAnimeAndCharacterService CreateAnimeAndCharacterService();
        IAnimeAndGenreService CreateAnimeAndGenreService();
        IAnimeAndStudioService CreateAnimeAndStudioService();

    }
}
