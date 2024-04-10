﻿using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAnimeAndStudioRepository : Intermediate<AnimeAndStudio, Anime, Studio>
    {
    }
}
