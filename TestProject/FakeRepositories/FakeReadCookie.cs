using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeReadCookie : IReadFromBrowser
    {
        User user = new User() { FirstName = "yahav", LastName = "hadad",
            Email = "yahav@gmaul.com", UserId = 1, UserName = "ani",
            Level = 1, Password = "MVC.Core", BirthDate = DateTime.Now };

        public string ReadSession(string name)
        {
            return null /*JsonConvert.SerializeObject(user)*/;
        }

        string IReadFromBrowser.ReadCookie(string name)
        {
            
            return JsonConvert.SerializeObject(user);
        }
    }
}
