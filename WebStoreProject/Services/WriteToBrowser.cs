using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace WebStoreProject.Services
{
    public class WriteToBrowser : IWriteToBrowser
    {
        private readonly IHttpContextAccessor _write;
        private readonly IConfiguration _configration;

        public WriteToBrowser(IHttpContextAccessor HttpContextAccessor,IConfiguration configration)
        {
            _write = HttpContextAccessor;
            _configration = configration;
        }

        //Cookies
        public void WriteCookies(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(double.Parse(_configration["RefreshDuration"]));
            _write.HttpContext.Response.Cookies.Append(key,value,option);
        }
        public void DeleteCookies(string name)
        {
            _write.HttpContext.Response.Cookies.Delete(name);
        }

        //Session
        public void WriteToSession(string key, string value)
        {
            _write.HttpContext.Session.SetString(key, value);

        }


        public void DeleteSession( string name)
        {
            _write.HttpContext.Session.Remove(name);
            
        }

      


    }
}
