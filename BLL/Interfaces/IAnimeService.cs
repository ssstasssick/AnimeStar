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
        public Task<AnimeDTO> LoadPageInf(AnimeDTO anime);
        public double CalculateAverageRating(List<ReviewDTO> reviews);
        void UpdateAverageRating(int animeId);
        int CreateWhithId(AnimeDTO entity);
        void UpdateAnimeGenres(AnimeDTO anime, List<int> genreIds);
        void UpdateAnimeCharacters(AnimeDTO anime, List<int> characterIds);
        void UpdateAnimeStudios(AnimeDTO anime, int studioId);
        public AnimeStatisticsDTO GetAnimeStatistics(int animeId);
    }
}
