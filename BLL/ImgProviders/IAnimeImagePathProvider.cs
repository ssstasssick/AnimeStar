using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ImgProviders
{
    public interface IAnimeImagePathProvider
    {
        string GetAnimeImagePath(string animeName);
        string GetStudioImagePath(string studioName);
        string GetCharacterImagePath(string characterName);
    }
}
