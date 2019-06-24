using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestProject.FakeRepositories;
using WebStoreProject.Models;
using WebStoreProject.Services;
using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Controllers;
using FluentAssertions;

namespace TestProject
{

    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexModelShouldContainAllProducts()
        {
            // Arrange
            IRepositoryProducts fakeProductRepository = new FakeProductRepository();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();

            HomeController homeController = new HomeController(fakeProductRepository, fakeReadCookie, fakeWriteCookie);

            // Act
            ViewResult viewResult = homeController.Index("name") as ViewResult;
            List<Product> products = viewResult.Model as List<Product>;

            // Should Be
            products.Count.Should().Be(4);
        }

        [TestMethod]
        public void IndexModelShouldContainTheResultOfTheSearch()
        {
            // Arrange
            IRepositoryProducts fakeProductRepository = new FakeProductRepository();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();

            HomeController homeController = new HomeController(fakeProductRepository, fakeReadCookie, fakeWriteCookie);

            // Act
            ViewResult viewResult = homeController.Search("T") as ViewResult;
            List<Product> products = viewResult.Model as List<Product>;

            // Should Be
            products.Count.Should().Be(2);
        }




    }
}
