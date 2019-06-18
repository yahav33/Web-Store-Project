using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
    public interface IEmailManger
    {
        void SendEmail(string content, string email, string subject);
    }
}
