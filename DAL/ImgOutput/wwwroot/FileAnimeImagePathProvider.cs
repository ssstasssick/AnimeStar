using DAL.ImgOutput.Interface;
using Microsoft.AspNetCore.Http;
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

        public string SaveImage(IFormFile imageFile, string path)
        {
            // Генерируем уникальное имя файла
            var uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(imageFile.FileName)}";

            // Формируем путь к файлу
            var filePath = "D:/STUDIES/curs3/2sem/Курсовая ИГИ/AnimeStar/AnimeStar/wwwroot" + _rootPath + path + uniqueFileName;

            // Сохраняем файл на сервере
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
