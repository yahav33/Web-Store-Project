using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Data;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class RepositoryUser : IRepositoryUser
    {
        StoreContext _context;
        IWriteToBrowser _write;
        IReadFromBrowser _read;

        public RepositoryUser(StoreContext context , IWriteToBrowser write, IReadFromBrowser readFrom)
        {
            _context = context;
            _write = write;
            _read = readFrom;

        }
        public bool IsUserAdmin(string userCoockie)
        {
            if (userCoockie == null) return false;
            User user = JsonConvert.DeserializeObject<User>(userCoockie);
            
            return user.Level > 0;
        }
        public User GetUserByUserName(string userName) => _context.Users.FirstOrDefault(x => x.UserName == userName);
        public bool Login(Login login)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == login.Username && x.Password == EncryptManager.EncryptPass(login.Password)) != null;
        }


        public bool Register(string userName)
        {
            if (_context.Users.FirstOrDefault(x => x.UserName == userName) != null)
            {
                // exist
                return false;
            }
            else
            { 
                //Not Exist
                return true;
            }
          
        }
        public User GetUser(int id)
        {
           return _context.Users.SingleOrDefault(u => u.UserId == id);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool EmailCheck(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username) == null;
        }
        public void SaveUsers()
        {
            _context.SaveChanges();

        }
        public bool UpdateUser(string userPP,Register register)
        {
            User userr = JsonConvert.DeserializeObject<User>(userPP);
            User user = _context.Users.FirstOrDefault(x => x.UserId == userr.UserId);
            if (user != null)
            {
                user.BirthDate = register.BirthDate;
                user.Email = register.Email;
                user.FirstName = register.FirstName;
                user.LastName = register.LastName;

                //check the null of the userName From register
                user.UserName = userr.UserName;
                user.Password = EncryptManager.EncryptPass(register.Password);

                string json = JsonConvert.SerializeObject(user);
                _write.WriteCookies("User", json);
                _write.WriteToSession("User", json);
            }
            return true;
           
        }

        public string GetUserName(string userCookie)
        {
            if (userCookie == null) return "New User";
            User user = JsonConvert.DeserializeObject<User>(userCookie);
            if(user== null)
            {
                return "New User";
            }
            return user.FirstName.First().ToString().ToUpper() + user.FirstName.Substring(1);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserFromCookie()
        {
            string user = _read.ReadCookie("User");
            if (user != null)
            {
                User newUser = JsonConvert.DeserializeObject<User>(user);
                return newUser;
            }
            return null;
        }

        public bool Login(string Password)
        {     
            var val = _context.Users.FirstOrDefault(x => x.Password.ToLower() == EncryptManager.EncryptPass(Password.ToLower()));
            return val != null;
        }
    }
}
