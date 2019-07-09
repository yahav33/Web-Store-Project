using System.Collections.Generic;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public interface IRepositoryUser
    {
        bool Register(string userName);
        bool Login(Login login);
        bool Login(string username);
        User GetUserByUserName(string userName);
        User GetUser(int id);
        void CreateUser(User user);
        bool EmailCheck(string email);
        bool UpdateUser(string userPp,Register register);
        void SaveUsers();
        bool IsUserAdmin(string userCoockie);
        string GetUserName(string userCookie);
        List<User> GetUsers();
        User GetUserFromCookie();


    }
}
