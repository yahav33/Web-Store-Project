using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void IndexModelShouldContainOnlyAvailableProducts()
        {
            // Arrange
            IRepositoryProducts fakeProductRepository = new FakeProductRepository();
            IReadFromBrowser fakeReadCookie = new FakeReadCookie();
            IWriteToBrowser fakeWriteCookie = new FakeWriteCookie();

            var homeController = new HomeController(fakeProductRepository, fakeReadCookie, fakeWriteCookie);

            // Act
            var viewResult = homeController.Index("name") as ViewResult;
            var products = viewResult.Model as List<Product>;

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

            var homeController = new HomeController(fakeProductRepository, fakeReadCookie, fakeWriteCookie);

            // Act
            var viewResult = homeController.Search("T") as ViewResult;
            var products = viewResult.Model as List<Product>;

            // Should Be
            products.Count.Should().Be(2);
        }

       

    }
}
