using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using WebStoreProject.Controllers;
using WebStoreProject.Services;
using TestProject.FakeRepositories;
using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Models;

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
            var login1 = new Login() { Username="yahaTv", Email="ani@yahho.com", Password="43232" }; 
            var loginController = new LoginController(fakeEmptyCart, fakeRepositoryUser,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 
            var redirectToActionResult = loginController.Login(login1) as RedirectToActionResult;

            // Example... Not sure it should be this way here.
            redirectToActionResult.Should().NotBeNull();
            redirectToActionResult.ControllerName.Should().Be("Login");
            redirectToActionResult.ActionName.Should().Be("index");
        }

        [TestMethod]
        public void LoginUserSuccess()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IEmailManger fakeEmailManger = new FakeEMailManger();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();
            IEmptyCart fakeEmptyCart = new FakeEmptyCart();
            ILogger fakeLogger = new FakeLogger();
            var login1 = new Login() { Username = "yahav", Email = "ani@yahho.com", Password = "43232" };
            var loginController = new LoginController(fakeEmptyCart, fakeRepositoryUser,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 
            var viewResult = loginController.Login(login1) as ViewResult;
            var rez = viewResult.Model;

            rez.Equals(new Login());
        }
    }
}
