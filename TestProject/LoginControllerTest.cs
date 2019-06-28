using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Controllers;
using WebStoreProject.Services;
using TestProject.FakeRepositories;
using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Models;
using FluentAssertions;

namespace TestProject
{
   
    [TestClass]  
    public class LoginControllerTest
    {
        [TestMethod]
        public void LoginUserFailed()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IEmailManger fakeEmailManger = new FakeEMailManger();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();
            IEmptyCart fakeEmptyCart = new FakeEmptyCart();
            ILogger fakeLogger = new FakeLogger();
            Login login1 = new Login() { Username="yahaTv", Email="ani@yahho.com", Password="43232" }; 
            LoginController loginController = new LoginController(fakeEmptyCart, fakeRepositoryUser,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 
            ViewResult viewResult = loginController.Login(login1) as ViewResult;
            var rez = viewResult.Model;

            rez.Equals(new List<Product>());
        }

        [TestMethod]
        public void LoginUserSuccsess()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IEmailManger fakeEmailManger = new FakeEMailManger();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();
            IEmptyCart fakeEmptyCart = new FakeEmptyCart();
            ILogger fakeLogger = new FakeLogger();
            Login login1 = new Login() { Username = "yahav", Email = "ani@yahho.com", Password = "43232" };
            LoginController loginController = new LoginController(fakeEmptyCart, fakeRepositoryUser,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 
            ViewResult viewResult = loginController.Login(login1) as ViewResult;
            var rez = viewResult.Model;

            rez.Equals(new Login());
        }
    }
}
