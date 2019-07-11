using System.Collections.Generic;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public interface IRepositoryProducts
    {
        List<Product> GetAllProducts();
        void AddProduct(Product product);
        Product GetProduct(long id);
        List<Product> GetCartProducts(List<long> productids);
        int SaveProducts();
        decimal TotalCardWorth(string ShoppingCart);
        decimal TotalCardWorthForMembers(string ShoppingCart);
        void BuyProducts(string ShoppingCart);
        void RemoveProduct(long id);
        List<Product> GetProductsOfUser(long userId);
        List<Product> Search(string searchKey);
    }
}
