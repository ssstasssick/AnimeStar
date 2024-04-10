using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ImgOutput.Interface
{
    public interface IAnimeImagePathProvider
    {
        string GetImagePath(string animeName);
    }
}
