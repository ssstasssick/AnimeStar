using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ImgProviders
{
    public class ImgProvider : IAnimeImagePathProvider
    {
        private readonly string _rootPath;

        public ImgProvider(string rootPath)
        {
            _rootPath = rootPath;
        }

        public string GetAnimeImagePath(string animeName)
        {
            return _rootPath + "/animes/" + animeName;
        }

        public string GetCharacterImagePath(string characterName)
        {
            return Path.Combine(_rootPath,"characters", characterName);
        }

        public string GetStudioImagePath(string studioName)
        {
            return Path.Combine(_rootPath,"studios",studioName);
        }
    }
}
