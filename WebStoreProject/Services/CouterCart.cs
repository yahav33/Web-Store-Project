using Newtonsoft.Json;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class CouterCart : ICounterCart
    {
        private readonly IReadFromBrowser _read;

        public CouterCart(IReadFromBrowser read)
        {
            _read = read;
        }

        public int GetNumOfProduct()
        {
            
            string jsonCart = _read.ReadCookie("Cart");
            if (jsonCart == null)return 0;
                ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(jsonCart);
            if (cart == null) return 0;
            return cart.ProductIDs.Count;
        }

    }
}
