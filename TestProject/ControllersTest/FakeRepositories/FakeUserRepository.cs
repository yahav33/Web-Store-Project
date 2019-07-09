using System;
using System.Collections.Generic;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    internal class FakeUserRepository : IRepositoryUser
    {
        public static List<User> Users = new List<User>()
        {
            new User{ UserId=1,UserName="first", Email="yt@yah.com", FirstName="tim", LastName="last", Level=1, Password="12345", BirthDate=DateTime.Now},
            new User{ UserId=1,UserName="second", Email="yt@yah.com", FirstName="timor", LastName="gio", Level=0, Password="123456", BirthDate=DateTime.Now},
            new User{ UserId=1,UserName="third", Email="yt@yah.com", FirstName="eli", LastName="hoe", Level=0, Password="123457", BirthDate=DateTime.Now},
            new User{ UserId=1,UserName="fifth", Email="yt@yah.com", FirstName="yafit", LastName="frtiy", Level=1, Password="123454", BirthDate=DateTime.Now},
            new User{ UserId=1,UserName="five", Email="yt@yah.com", FirstName="green", LastName="ertou", Level=0, Password="123452", BirthDate=DateTime.Now}

        };

        public void CreateUser(User user)
        {
            Users.Add(user);
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
            return Users.Find(n => n.UserName == userName);
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
            if (login.Username.Contains('T')) return true;
            return false;
        }

        public bool Login(string username)
        {
            if(username == "UserIsLogin")
            {
                return true;
            }
            return false;
        }

        public bool Register(string userName)
        {
            var rez = Users.Find(u => u.UserName == userName);
            if (rez == null) return true;
            return false;
        }

        public void SaveUsers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(string userPp, Register register)
        {
            throw new NotImplementedException();
        }
    }
}
