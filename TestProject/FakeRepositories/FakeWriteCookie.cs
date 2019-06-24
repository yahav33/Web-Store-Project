using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeWriteCookie : IWriteToBrowser
    {
        public void DeleteCookies(string name)
        {
            
        }

        public void DeleteSession(string name)
        {
           
        }

        public void WriteCookies(string key, string value)
        {
          
        }

        public void WriteToSession(string key, string value)
        {
            
        }
    }
}
