using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
    public class ReadForBrowser : IReadFromBrowser
    {
        private IHttpContextAccessor _read;

        public ReadForBrowser(IHttpContextAccessor HttpContextAccessor)
        {
            _read = HttpContextAccessor;
        }

        public string ReadSession(string key)
        {
            string value = _read.HttpContext.Session.GetString(key);

            return value;
        }

        public string ReadCookie(string key)
        {
            string value = _read.HttpContext.Request.Cookies[key];
            return value;
        }
             
    }
}
