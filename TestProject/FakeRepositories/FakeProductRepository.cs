using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeProductRepository : IRepositoryProducts
    {
        public static List<Product> Products = new List<Product>()
        {
            new Product{ ProductId=1, UserId=1, Price=23, Title="NewOne"},
             new Product{ ProductId=2, UserId=2, Price=56, Title="NewTwo"},
              new Product{ ProductId=3, UserId=3, Price=89, Title="NewThird"},
               new Product{ ProductId=4, UserId=6, Price=90, Title="NewForth"}

        };

        void IRepositoryProducts.AddProduct(Product product)
        {
            Products.Add(product);
        }

        void IRepositoryProducts.BuyProducts(string ShoppingCart)
        {
            throw new NotImplementedException();
        }

        List<Product> IRepositoryProducts.GetAllProducts()
        {
            return Products;
        }

        List<Product> IRepositoryProducts.GetCartProducts(List<long> productids)
        {
            List<Product> tempProducts = new List<Product>();
            for (int i = 0; i < productids.Count; i++)
            {
                var product = Products.Find(x => x.ProductId == productids[i]);
                if (product != null) tempProducts.Add(product);
            }
            return tempProducts;
        }

        Product IRepositoryProducts.GetProduct(long id)
        {
            var product = Products.Find(x => x.ProductId == id);
            return product;
        }

        List<Product> IRepositoryProducts.GetProductsOfUser(long userId)
        {
            List<Product> tempProducts = new List<Product>();
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].UserId == userId) tempProducts.Add(Products[i]);
            }
            return tempProducts;
        }

        void IRepositoryProducts.RemoveProduct(long id)
        {
            var product = Products.Find(x => x.ProductId == id);
            Products.Remove(product);
        }

        //not relent 
        int IRepositoryProducts.SaveProducts()
        {
            throw new NotImplementedException();
        }

        List<Product> IRepositoryProducts.Search(string searchKey)
        {
            return Products.Where(x => x.Title.Contains(searchKey)).ToList();
        }

        decimal IRepositoryProducts.TotalCardWorth(string ShoppingCart)
        {
            throw new NotImplementedException();
        }

        decimal IRepositoryProducts.TotalCardWorthForMembers(string ShoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
