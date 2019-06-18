using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
    public interface IReadFromBrowser
    {
        string ReadSession(string name);
        string ReadCookie(string name);
    }
}
