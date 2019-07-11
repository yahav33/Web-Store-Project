using System;
using System.ComponentModel.DataAnnotations;

namespace WebStoreProject.Models
{
    public class Product
    {
        [Required]
        public long ProductId { get; set; }

        public long? OwnerId { get; set; } //FK - the buyer

        public long? UserId { get; set; } //FK - the seller

        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string LongDescription { get; set; }
    
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime TimeStamp { get; set; }
        [Required]
        public decimal Price { get; set; }

        public byte[] ImageOne { get; set; }
        public byte[] ImageTwo { get; set; }
        public byte[] ImageThree { get; set; }
        
        public StatusState Status { get; set; } = StatusState.In_Stock;
      
        public virtual User User { get; set; } //FK
        public virtual User Owner { get; set; } //FK
    }
    
    public enum StatusState
    {
        In_Stock,
        In_Cart,
        Purchased
    }
}

