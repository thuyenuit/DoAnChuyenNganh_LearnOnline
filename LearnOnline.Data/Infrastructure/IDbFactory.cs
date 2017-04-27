﻿using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOnline.Data.Infrastructure
{
    public interface IDbFactory: IDisposable
    {
        LearnOnlineDbContext Init();
    }
}
