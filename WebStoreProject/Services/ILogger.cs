﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
      public interface ILogger
    {
        void WriteLog(string strLog, Catgory catgory);
    }
}
