﻿using BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IForumService : IService<ForumDTO>
    {
        Task<ForumDTO> LoadPageInf(ForumDTO forum);
        Task<IEnumerable<ForumDTO>> GetBestAsync(int count);
    }
}
