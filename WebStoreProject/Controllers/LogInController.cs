using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRepositoryUser _irepositoryUser;
        private readonly IReadFromBrowser _read;
        private readonly IWriteToBrowser _write;
        private readonly IEmptyCart _emptyCart;
        private readonly IEmailManger _sendEmail;
        private readonly ILogger _logger;

        public LoginController(IEmptyCart emptyCart,IRepositoryUser irepositoryUser, 
            IReadFromBrowser read, IWriteToBrowser write, IEmailManger email,ILogger logger)
        {
            _irepositoryUser = irepositoryUser;
            _read = read;
            _write = write;
            _emptyCart = emptyCart;
            _sendEmail = email;
            _logger = logger;
        }
        public IActionResult Index()
        {
            
            string User = _read.ReadSession("User");
           
            if (User != null)
            {
                return RedirectToAction("Index","Home");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
         
            if (_irepositoryUser.Login(login))
            {
                //User Login
               
                var userNote = JsonConvert.SerializeObject(_irepositoryUser.GetUserByUserName(login.Username));
                _write.WriteToSession("User", userNote);
                _write.WriteCookies("User", userNote);
               _logger.WriteLog($"user log in : {login.Username}", Catgory.User);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Player Not Login
                return RedirectToAction("index", "Login");
            }
        }

        [HttpPost]
        public JsonResult LoginChecker(string Password)
        {

            return Json(_irepositoryUser.Login(Password));
        }

        public IActionResult ForgotPassword()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(Login login)
        {
            var user = _irepositoryUser.GetUserByUserName(login.Username);
            if(user != null)
            {
                if (user.Email == login.Email && user.BirthDate == login.BirthDate)
                {
                    string message = "Your Password is : "+EncryptManager.EncryptPass(user.Password);
                    _sendEmail.SendEmail(message, user.Email, "Your Password");
                    _logger.WriteLog($"User : {login.Username} - reconstruction his Password",Catgory.User);

                }
            }

            return RedirectToAction("index", "login");
           
           
        }
        
        public IActionResult Logout()
        {
            _logger.WriteLog($"User :{_read.ReadCookie("user")} ", Catgory.User);
            _write.DeleteCookies("User");
            _write.DeleteSession("User");
            _emptyCart.EmptyCart(_read.ReadCookie("Cart"));
            _write.DeleteCookies("Cart"); 
            return RedirectToAction("Index", "Home");
        }
    }
}