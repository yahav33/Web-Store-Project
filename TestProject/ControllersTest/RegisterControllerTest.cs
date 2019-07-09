using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.FakeRepositories;
using WebStoreProject.Controllers;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace TestProject
{
    [TestClass]
    public class RegisterControllerTest
    {
        [TestMethod]
        public void RegisterUserSuccess()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IEmailManger fakeEmailManger = new FakeEMailManger();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();
            ICheckTime checkTime = new CheckTime();
            ILogger fakeLogger = new FakeLogger();

            var register1 = new Register() { UserName = "Roy", Email = "ani@yahho.com",
                Password = "43232", FirstName="Retif", LastName= "Teruy" };

            var loginController = new RegisterController
                (fakeRepositoryUser,checkTime,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 

            var viewResult = loginController.Register(register1) as ViewResult;
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

            var register1 = new Register()
            {
                UserName = "Roy",
                Email = "ani@yahho.com",
                Password = "43232",
                FirstName = "Retif",
                LastName = "Teruy"
            };

            var loginController = new RegisterController
                (fakeRepositoryUser, checkTime,
                fakeReadCookie, fakeWriteCookie, fakeEmailManger, fakeLogger);

            // Act 

            var viewResult = loginController.Register(register1) as ViewResult;
            var rez = viewResult.Model;

            rez.Equals(new Register());
        }

    }
}
