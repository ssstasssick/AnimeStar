using Microsoft.AspNetCore.Http;
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
        private readonly DAL.ImgOutput.Interface.IAnimeImagePathProvider _provider; 
        
        public ImgProvider(string rootPath, DAL.ImgOutput.Interface.IAnimeImagePathProvider provider)
        {
            _rootPath = rootPath;
            _provider = provider;
        }

        public string GetAnimeImagePath(string animeName)
        {
            
            return _rootPath + "animes/" + animeName;
        }

        public string GetCharacterImagePath(string characterName)
        {
            return _rootPath + "characters/" + characterName;
        }

        public string GetStudioImagePath(string studioName)
        {
            return _rootPath + "studios/" + studioName;
        }

        public string SaveAnimeImg(IFormFile imageFile)
        {
            return _provider.SaveImage(imageFile, "animes/");
        }

        public string SaveCharacterImg(IFormFile imageFile)
        {
            return _provider.SaveImage(imageFile, "characters/");
        }

        public string SaveStudioImg(IFormFile imageFile)
        {
            return _provider.SaveImage(imageFile, "studios/");
        }
    }
}
