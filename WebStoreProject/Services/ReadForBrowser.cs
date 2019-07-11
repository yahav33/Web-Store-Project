using Microsoft.AspNetCore.Http;

namespace WebStoreProject.Services
{
    public class ReadForBrowser : IReadFromBrowser
    {
        private readonly IHttpContextAccessor _read;

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
