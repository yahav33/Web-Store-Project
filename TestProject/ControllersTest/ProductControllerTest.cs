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
using Microsoft.AspNetCore.Http;

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

           
            ProductController productController = new ProductController(fakeRepositoryProducts, fakeRepositoryUser,
                fakeReadCookie, fakeCheckUser,fakeLogger);

            // Act 
            ViewResult viewResult = productController.MyProducts() as ViewResult;
            List<Product> products = viewResult.Model as List<Product>;

            // Should Be
            products.Count.Should().Be(2);
        }

       
    }
}
