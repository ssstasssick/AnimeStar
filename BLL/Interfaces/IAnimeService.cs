using BLL.Entity;
using BLL.ImgProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnimeService : IService<AnimeDTO>
    {
        public IEnumerable<AnimeDTO> GetBest(int animeCount);
        public IEnumerable<AnimeDTO> GetLatest(int animeCount);
        public AnimeDTO ConnectImg(IAnimeImagePathProvider animeImagePathProvider, AnimeDTO animeList);
        public AnimeDTO LoadPageInf(AnimeDTO anime);
    }
}
