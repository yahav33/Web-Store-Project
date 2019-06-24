using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeUserRepository : IRepositoryUser
    {
        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool EmailCheck(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUserFromCookie()
        {
            throw new NotImplementedException();
        }

        public string GetUserName(string userCookie)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool IsUserAdmin(string userCoockie)
        {
            throw new NotImplementedException();
        }

        public bool Login(Login login)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username)
        {
            throw new NotImplementedException();
        }

        public bool Register(string userName)
        {
            throw new NotImplementedException();
        }

        public void SaveUsers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(string userPP, Register register)
        {
            throw new NotImplementedException();
        }
    }
}
