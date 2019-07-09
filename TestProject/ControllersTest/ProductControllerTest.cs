using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestProject.FakeRepositories;
using WebStoreProject.Controllers;
using WebStoreProject.Models;
using WebStoreProject.Services;
using FluentAssertions;

namespace TestProject
{
    [TestClass]
     public class ProductControllerTest
     {
        [TestMethod]
        public void GetAllItemOfSpsipicUser()
        {
            // Arrange
            IRepositoryUser fakeRepositoryUser = new FakeUserRepository();
            IRepositoryProducts fakeRepositoryProducts = new FakeProductRepository();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            ICheckUserExist fakeCheckUser = new FakeCheckUser();
            ILogger fakeLogger = new FakeLogger();

           
            var productController = new ProductController(fakeRepositoryProducts, fakeRepositoryUser,
                fakeReadCookie, fakeCheckUser,fakeLogger);

            // Act 
            var viewResult = productController.MyProducts() as ViewResult;
            var products = viewResult.Model as List<Product>;

            // Should Be
            products.Count.Should().Be(2);
        }

       
    }
}
