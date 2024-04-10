using BLL.Entity;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnimeAndGenreService : Intermediate<AnimeAndGenreDTO, AnimeDTO, GenreDTO>
    {
    }
}
