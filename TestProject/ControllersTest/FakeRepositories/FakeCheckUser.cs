using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeCheckUser : ICheckUserExist
    {
        public bool CheckUserLogin()
        {
            return true;
        }
    }
}
