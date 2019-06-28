using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeEMailManger : IEmailManger
    {
        public void SendEmail(string content, string email, string subject, string path = null)
        {
            
        }
    }
}
