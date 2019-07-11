using System;
using System.Collections.Generic;

namespace WebStoreProject.Models
{
    public class ShoppingCart
    {
        public long UserID { get; set; } // User id
        public List<long> ProductIDs { get; set; } // List of products Id
        public DateTime InitCartTime { get; set; } // Lasts for 30 days
       
    }
}
