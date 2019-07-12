
using Xunit;
using WebStoreProject.Services;
using WebStoreProject.Models;
using TestProject.FakeRepositories;
using System.Collections.Generic;

namespace TestProjectUsingXUnit
{
    public class CartMangerTest
    {
        [Fact]
        public void ShouldReturnTheCountOfTheIDs()
        {
            // Arrange
            IReadFromBrowser read = new FakeReadCookie();
            CartManager cart = new CartManager(read);
            List<long> listID = new List<long>() { 23, 45, 32, 12, 2, 3 };
            ShoppingCart shoppingCart = new ShoppingCart() { ProductIDs = listID, UserID = 3 };

            //Act

            var IdsList = cart.GetCartProducts(shoppingCart);

            //Assert

            Assert.Equal<long>(IdsList, listID);

        }

        [Fact]
        public void ShouldReturnTheNewCartAfterRemove()
        {
            // Arrange
            IReadFromBrowser read = new FakeReadCookie();
            CartManager cart = new CartManager(read);
            List<long> listID = new List<long>() { 23, 45, 32, 12, 2, 3 };
            ShoppingCart shoppingCart = new ShoppingCart() { ProductIDs = listID, UserID = 3 };

            //Act

            var currentCart = cart.RemoveProduct(45,shoppingCart);

            //Assert

            Assert.DoesNotContain<long>(45,currentCart.ProductIDs);

        }

        [Fact]
        public void ShouldReturnTheNewCartAfterAddID()
        {
            // Arrange
            IReadFromBrowser read = new FakeReadCookie();
            CartManager cart = new CartManager(read);
            List<long> listID = new List<long>() { 23, 45, 32, 12, 2, 3 };
            ShoppingCart shoppingCart = new ShoppingCart() { ProductIDs = listID, UserID = 3 };

            //Act

            var currentCart = cart.AddProduct(90, shoppingCart);

            //Assert

            Assert.Contains<long>(90, currentCart.ProductIDs);

        }
    }
}
