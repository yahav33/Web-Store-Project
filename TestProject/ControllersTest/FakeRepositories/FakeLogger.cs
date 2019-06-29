using WebStoreProject.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.FakeRepositories
{
    class FakeLogger : ILogger
    {
        void ILogger.WriteLog(string strLog, Catgory catgory)
        {
            
        }
    }
}
