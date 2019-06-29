using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Data;
using WebStoreProject.Models;
using WebStoreProject.Services;


namespace WebStoreProject.Controllers
{
    public class RegisterController : Controller
    {
        IRepositoryUser _irepositoryUser;
        ICheckTime _checktime;
        IReadFromBrowser _read;
        IWriteToBrowser _write;
        IEmailManger _send;
        ILogger _logger;

        public RegisterController(IRepositoryUser irepositoryUser,ICheckTime checktime
            , IReadFromBrowser read, IWriteToBrowser write,
            IEmailManger email, ILogger logger)
        {
             _irepositoryUser = irepositoryUser;
            _checktime = checktime;
             _read = read;
            _write = write;
            _send = email;
            _logger = logger;
        }
        public IActionResult Index()
        {
            string User = _read.ReadSession("User");
            User userData = null;
            Register reg = null;
            if (User != null)
            {
                userData = JsonConvert.DeserializeObject<User>(User);
                 reg = new Register {
                    FirstName = userData.FirstName,
                    BirthDate = userData.BirthDate,
                    Email = userData.Email,
                    LastName = userData.LastName,
                    UserName = userData.UserName,
                };
            }
            if (reg != null)
            {
                return View(reg);
            }
            return View();
        }

        [HttpPost]
        public JsonResult CheckBirthdate(DateTime BirthDate)
        {

            return Json(_checktime.DateValidation(BirthDate));
        }

        [HttpPost]
        public JsonResult CheckUser(string UserName)
        {
            
            return Json(_irepositoryUser.EmailCheck(UserName));
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
           
            string UserPP = _read.ReadSession("User");
            if (UserPP != null)
            {
                _irepositoryUser.UpdateUser(UserPP, register);
                _irepositoryUser.SaveUsers();
                _logger.WriteLog($"User Updated : {register.UserName}",Catgory.User);
                return RedirectToAction("index", "Home");
            }

            else
            {
                if (_irepositoryUser.Register(register.UserName))
                {
                    User user = new User
                    {
                        BirthDate = register.BirthDate,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Level = 0,
                        Password = EncryptManager.EncryptPass(register.Password),
                        UserName = register.UserName


                    };

                    _irepositoryUser.CreateUser(user);
                    string emailcontent = String.Format("<h2>Hello {0}, welcome to our Shop!!!</h2> <a href='https://localhost:44347/'>Click here to get the Best Price's!!</a> ", user.FirstName);
                    _send.SendEmail(emailcontent, user.Email, "Welcome To Store on-Line");
                    _logger.WriteLog($"User Register : {register.UserName}", Catgory.User);
                    return RedirectToAction("index", "login");
                }
                else
                {
                    return RedirectToAction("index", "Register");
                }

            }
           

            
        }

    }
}