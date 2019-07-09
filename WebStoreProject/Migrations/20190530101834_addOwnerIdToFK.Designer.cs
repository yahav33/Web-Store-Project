﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebStoreProject.Data;

namespace WebStoreProject.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20190530101834_addOwnerIdToFK")]
    partial class AddOwnerIdToFk
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebStoreProject.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<byte[]>("ImageOne");

                    b.Property<byte[]>("ImageThree");

                    b.Property<byte[]>("ImageTwo");

                    b.Property<string>("LongDescription")
                        .IsRequired();

                    b.Property<long?>("OwnerId");

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<long?>("UserId");

                    b.HasKey("ProductId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");

                    b.HasData(
                        new { ProductId = 1L, Date = new DateTime(2019, 5, 30, 13, 18, 33, 544, DateTimeKind.Local), LongDescription = "nice car", OwnerId = 1L, Price = 10m, ShortDescription = "cool", Status = 0, Title = "Ferari", UserId = 1L },
                        new { ProductId = 2L, Date = new DateTime(2019, 5, 30, 13, 18, 33, 544, DateTimeKind.Local), LongDescription = "cool car", OwnerId = 1L, Price = 100m, ShortDescription = "nice", Status = 0, Title = "Lamburgini", UserId = 1L },
                        new { ProductId = 3L, Date = new DateTime(2019, 5, 30, 13, 18, 33, 544, DateTimeKind.Local), LongDescription = "Dope car", OwnerId = 1L, Price = 1200m, ShortDescription = "Epic", Status = 0, Title = "Bugatti", UserId = 2L }
                    );
                });

            modelBuilder.Entity("WebStoreProject.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<byte>("Level");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new { UserId = 1L, BirthDate = new DateTime(2019, 5, 30, 13, 18, 33, 540, DateTimeKind.Local), Email = "Heyyy@gmail.com", FirstName = "nice car", LastName = "cool", Level = (byte)1, Password = "10", UserName = "fsd" },
                        new { UserId = 2L, BirthDate = new DateTime(2019, 5, 30, 13, 18, 33, 542, DateTimeKind.Local), Email = "Heyyy@gmail.com", FirstName = "nice car", LastName = "cool", Level = (byte)1, Password = "10", UserName = "fsd" }
                    );
                });

            modelBuilder.Entity("WebStoreProject.Models.Product", b =>
                {
                    b.HasOne("WebStoreProject.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("WebStoreProject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
