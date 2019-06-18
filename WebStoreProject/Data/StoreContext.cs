using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Models;

namespace WebStoreProject.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
          new User { UserId = 1, Password = "10", Level = 1, FirstName = "nice car", LastName = "cool",Email="Heyyy@gmail.com",UserName = "fsd",BirthDate = DateTime.Now, },
          new User { UserId = 2, Password = "10", Level = 1, FirstName = "nice car", LastName = "cool", Email = "Heyyy@gmail.com", UserName = "fsd", BirthDate = DateTime.Now }
);

            modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Price = 10, UserId = 1,OwnerId = 2 ,Title = "Ferari", LongDescription = "nice car", ShortDescription = "cool"},
            new Product { ProductId = 2, Price = 100, UserId = 1, OwnerId = 2, Title = "Lamburgini", LongDescription = "cool car", ShortDescription = "nice" },
            new Product { ProductId = 3, Price = 1200, UserId = 2, OwnerId = 2, Title = "Bugatti", LongDescription = "Dope car", ShortDescription = "Epic" }
       );

            
        }

    }
}

