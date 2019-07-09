using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Models;
using WebStoreProject.Services;


namespace WebStoreProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly ICheckTime _checkTime;
        private readonly IReadFromBrowser _read;
        private IWriteToBrowser _write;
        private readonly IEmailManger _send;
        private readonly ILogger _logger;

        public RegisterController(IRepositoryUser repositoryUser,ICheckTime checkTime
            , IReadFromBrowser read, IWriteToBrowser write,
            IEmailManger email, ILogger logger)
        {
             _repositoryUser = repositoryUser;
            _checkTime = checkTime;
             _read = read;
            _write = write;
            _send = email;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var user = _read.ReadSession("User");
            Register reg = null;
            if (user != null)
            {
                var userData = JsonConvert.DeserializeObject<User>(user);
                reg = new Register {
                    FirstName = userData.FirstName,
                    BirthDate = userData.BirthDate,
                    Email = userData.Email,
                    LastName = userData.LastName,
                    UserName = userData.UserName,
                };
            }

            if (reg != null)
                return View(reg);

            return View();
        }

        [HttpPost]
        public JsonResult CheckBirthdate(DateTime birthDate)
        {

            return Json(_checkTime.DateValidation(birthDate));
        }

        [HttpPost]
        public JsonResult CheckUser(string userName)
        {
            
            return Json(_repositoryUser.EmailCheck(userName));
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            var userPp = _read.ReadSession("User");
            if (userPp != null)
            {
                _repositoryUser.UpdateUser(userPp, register);
                _repositoryUser.SaveUsers();
                _logger.WriteLog($"User Updated : {register.UserName}",Catgory.User);
                return RedirectToAction("index", "Home");
            }
            else
            {
                if (_repositoryUser.Register(register.UserName))
                {
                    var user = new User
                    {
                        BirthDate = register.BirthDate,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Level = 0,
                        Password = EncryptManager.EncryptPass(register.Password),
                        UserName = register.UserName


                    };

                    _repositoryUser.CreateUser(user);
                    var emailContent = string.Format("<h2>Hello {0}, welcome to our Shop!!!</h2> <a href='https://localhost:44347/'>Click here to get the Best Price's!!</a> ", user.FirstName);
                    _send.SendEmail(emailContent, user.Email, "Welcome To Store on-Line");
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