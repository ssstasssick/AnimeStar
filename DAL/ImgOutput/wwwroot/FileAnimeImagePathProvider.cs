using DAL.ImgOutput.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ImgOutput.wwwroot
{
    public class FileAnimeImagePathProvider : IAnimeImagePathProvider
    {
        private readonly string _rootPath;
        public FileAnimeImagePathProvider(string rootPath)
        {
            _rootPath = rootPath;
        }
        public string GetImagePath(string animeName)
        {
            return Path.Combine(_rootPath, animeName);
        }
    }
}
