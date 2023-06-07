﻿// <auto-generated />
using System;
using JamesJonesDbs2.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JamesJonesDbs2.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JamesJonesDbs2.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmailAddress = "sean@sean",
                            PasswordHash = "$2a$11$1gEP4pCF0zWB118NxJ8pD./k9qtEwV5kGBYXC8jhfWlNh6wP9FCsK",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            EmailAddress = "kyle@kyle",
                            PasswordHash = "$2a$11$XhsjDGmqOyIWjuKdpJeZm.ysQ7b2KSheGSQNWG7jjMalUcIyAF/Vq",
                            Role = "Customer"
                        });
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.SamsWareHouseItem", b =>
                {
                    b.Property<int>("SamsWareHouseItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SamsWareHouseItemId"), 1L, 1);

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("SamsWareHouseItemId");

                    b.ToTable("SamsWareHouseItems");

                    b.HasData(
                        new
                        {
                            SamsWareHouseItemId = 1,
                            ItemName = "Granny Smith Apples",
                            Unit = "1kg",
                            UnitPrice = 5.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 2,
                            ItemName = "Fresh Tomatoes",
                            Unit = "500g",
                            UnitPrice = 5.9000000000000004
                        },
                        new
                        {
                            SamsWareHouseItemId = 3,
                            ItemName = "Watermelon",
                            Unit = "Whole",
                            UnitPrice = 6.5999999999999996
                        },
                        new
                        {
                            SamsWareHouseItemId = 4,
                            ItemName = "Cucumber",
                            Unit = "1 whole",
                            UnitPrice = 1.8999999999999999
                        },
                        new
                        {
                            SamsWareHouseItemId = 5,
                            ItemName = "Red Potato Washed",
                            Unit = "1kg",
                            UnitPrice = 4.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 6,
                            ItemName = "Red Tipped Bananas",
                            Unit = "1kg",
                            UnitPrice = 4.9000000000000004
                        },
                        new
                        {
                            SamsWareHouseItemId = 7,
                            ItemName = "Red Onion",
                            Unit = "1kg",
                            UnitPrice = 3.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 8,
                            ItemName = "Carrots",
                            Unit = "1kg",
                            UnitPrice = 2.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 9,
                            ItemName = "Iceburg Lettuce",
                            Unit = "1",
                            UnitPrice = 2.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 10,
                            ItemName = "Helga's Wholemeal",
                            Unit = "1",
                            UnitPrice = 3.7000000000000002
                        },
                        new
                        {
                            SamsWareHouseItemId = 11,
                            ItemName = "Free Range Chicken",
                            Unit = "1kg",
                            UnitPrice = 7.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 12,
                            ItemName = "Manning Valley 6-pk",
                            Unit = "6 eggs",
                            UnitPrice = 3.6000000000000001
                        },
                        new
                        {
                            SamsWareHouseItemId = 13,
                            ItemName = "A2 Light Milk",
                            Unit = "1 litre",
                            UnitPrice = 2.8999999999999999
                        },
                        new
                        {
                            SamsWareHouseItemId = 14,
                            ItemName = "Chobani Strawberry Yoghurt",
                            Unit = "1",
                            UnitPrice = 1.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 15,
                            ItemName = "Lurpark Salted Blend",
                            Unit = "250g",
                            UnitPrice = 5.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 16,
                            ItemName = "Bega Farmers Tasty",
                            Unit = "250g",
                            UnitPrice = 4.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 17,
                            ItemName = "Babybel Mini",
                            Unit = "100g",
                            UnitPrice = 4.2000000000000002
                        },
                        new
                        {
                            SamsWareHouseItemId = 18,
                            ItemName = "Cobram EVOO",
                            Unit = "375ml",
                            UnitPrice = 8.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 19,
                            ItemName = "Heinz Tomato Soup",
                            Unit = "535g",
                            UnitPrice = 2.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 20,
                            ItemName = "John West Tuna can",
                            Unit = "95g",
                            UnitPrice = 1.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 21,
                            ItemName = "Cadbury Dairy Milk",
                            Unit = "200g",
                            UnitPrice = 5.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 22,
                            ItemName = "Coca Cola",
                            Unit = "2 litre",
                            UnitPrice = 2.8500000000000001
                        },
                        new
                        {
                            SamsWareHouseItemId = 23,
                            ItemName = "Smith's Original Share Pack Crisps",
                            Unit = "170g",
                            UnitPrice = 3.29
                        },
                        new
                        {
                            SamsWareHouseItemId = 24,
                            ItemName = "Birds Eye Fish Fingers",
                            Unit = "375g",
                            UnitPrice = 4.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 25,
                            ItemName = "Berri Orange Juice",
                            Unit = "2 litre",
                            UnitPrice = 6.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 26,
                            ItemName = "Vegemite",
                            Unit = "380g",
                            UnitPrice = 6.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 27,
                            ItemName = "Cheddar Shapes",
                            Unit = "175g",
                            UnitPrice = 2.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 28,
                            ItemName = "Colgate Total ToothPaste",
                            Unit = "110g",
                            UnitPrice = 3.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 29,
                            ItemName = "Milo Chocolate Malt",
                            Unit = "200g",
                            UnitPrice = 4.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 30,
                            ItemName = "Weet Bix Saniatarium Organic",
                            Unit = "750g",
                            UnitPrice = 5.3300000000000001
                        },
                        new
                        {
                            SamsWareHouseItemId = 31,
                            ItemName = "Lindt Excellence 70% Cocoa Block",
                            Unit = "100g",
                            UnitPrice = 4.25
                        },
                        new
                        {
                            SamsWareHouseItemId = 32,
                            ItemName = "Original Tim Tams Chocolate",
                            Unit = "200g",
                            UnitPrice = 3.6499999999999999
                        },
                        new
                        {
                            SamsWareHouseItemId = 33,
                            ItemName = "Philadelphia Original Cream Cheese",
                            Unit = "250g",
                            UnitPrice = 4.2999999999999998
                        },
                        new
                        {
                            SamsWareHouseItemId = 34,
                            ItemName = "Moccona Classic Instant Medium Roast",
                            Unit = "100g",
                            UnitPrice = 6.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 35,
                            ItemName = "Capilano Sqeezable Honey",
                            Unit = "500g",
                            UnitPrice = 7.3499999999999996
                        },
                        new
                        {
                            SamsWareHouseItemId = 36,
                            ItemName = "Nutella Jar",
                            Unit = "400g",
                            UnitPrice = 4.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 37,
                            ItemName = "Arnott's Scotch Finger",
                            Unit = "250g",
                            UnitPrice = 2.8500000000000001
                        },
                        new
                        {
                            SamsWareHouseItemId = 38,
                            ItemName = "South Cape Greek Feta",
                            Unit = "200g",
                            UnitPrice = 5.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 39,
                            ItemName = "Sacla Pasta Tomato Basil Sauce",
                            Unit = "420g",
                            UnitPrice = 4.5
                        },
                        new
                        {
                            SamsWareHouseItemId = 40,
                            ItemName = "Primo English Ham",
                            Unit = "100g",
                            UnitPrice = 3.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 41,
                            ItemName = "Primo Short Cut Rindless Bacon",
                            Unit = "175g",
                            UnitPrice = 5.0
                        },
                        new
                        {
                            SamsWareHouseItemId = 42,
                            ItemName = "Golden Circle Pinapple Pieces in natural juice",
                            Unit = "440g",
                            UnitPrice = 3.25
                        },
                        new
                        {
                            SamsWareHouseItemId = 43,
                            ItemName = "San Renmo Linguine Pasta No 1",
                            Unit = "500g",
                            UnitPrice = 1.95
                        });
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.ShoppingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserID");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.ShoppingListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SamsWareHouseItemId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SamsWareHouseItemId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("ShoppingListItems");
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.ShoppingList", b =>
                {
                    b.HasOne("JamesJonesDbs2.Models.AppUser", "AppUser")
                        .WithMany("ShoppingList")
                        .HasForeignKey("AppUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.ShoppingListItem", b =>
                {
                    b.HasOne("JamesJonesDbs2.Models.SamsWareHouseItem", "SamsWareHouseItem")
                        .WithMany("ShoppingListItems")
                        .HasForeignKey("SamsWareHouseItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JamesJonesDbs2.Models.ShoppingList", "ShoppingList")
                        .WithMany("ShoppingListItems")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SamsWareHouseItem");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.AppUser", b =>
                {
                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.SamsWareHouseItem", b =>
                {
                    b.Navigation("ShoppingListItems");
                });

            modelBuilder.Entity("JamesJonesDbs2.Models.ShoppingList", b =>
                {
                    b.Navigation("ShoppingListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
