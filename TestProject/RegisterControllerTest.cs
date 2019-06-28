using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.FakeRepositories;
using WebStoreProject.Controllers;
using WebStoreProject.Models;
using WebStoreProject.Services;
using FluentAssertions;

namespace TestProject
{
    [TestClass]
    public class RegisterControllerTest
    {
        [TestMethod]
        public void RegisterUserSuccsess()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IEmailManger fakeEmailManger = new FakeEMailManger();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();
            ICheckTime checkTime = new CheckTime();
            ILogger fakeLogger = new FakeLogger();

            Register register1 = new Register() { UserName = "Roy", Email = "ani@yahho.com",
                Password = "43232", FirstName="Retif", LastName= "Teruy" };

            RegisterController loginController = new RegisterController
                (fakeRepositoryUser,checkTime,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 

            ViewResult viewResult = loginController.Register(register1) as ViewResult;
            var rez = viewResult.Model;

            rez.Equals(new Login());
        }


        [TestMethod]
        public void RegisterUserFailed()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IEmailManger fakeEmailManger = new FakeEMailManger();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();
            ICheckTime checkTime = new CheckTime();
            ILogger fakeLogger = new FakeLogger();

            Register register1 = new Register()
            {
                UserName = "Roy",
                Email = "ani@yahho.com",
                Password = "43232",
                FirstName = "Retif",
                LastName = "Teruy"
            };

            RegisterController loginController = new RegisterController
                (fakeRepositoryUser, checkTime,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 

            ViewResult viewResult = loginController.Register(register1) as ViewResult;
            var rez = viewResult.Model;

            rez.Equals(new Register());
        }

    }
}
