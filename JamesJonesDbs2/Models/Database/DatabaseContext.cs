using Microsoft.EntityFrameworkCore;

namespace JamesJonesDbs2.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        //Sequencial error, set them as DbSet before ModelCreating
        public DbSet<SamsWareHouseItem> SamsWareHouseItems { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    EmailAddress = "sean@sean",
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("sean"),
                    Role = "Admin"
                },

                new AppUser
                {
                    Id = 2,
                    EmailAddress = "kyle@kyle",
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("kyle"),
                    Role = "Customer"
                });

            modelBuilder.Entity<SamsWareHouseItem>().HasData(

                /**Im not proud of this myself**/
                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 1,
                    ItemName = "Granny Smith Apples",
                    Unit = "1kg",
                    UnitPrice = 5.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 2,
                    ItemName = "Fresh Tomatoes",
                    Unit = "500g",
                    UnitPrice = 5.90,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 3,
                    ItemName = "Watermelon",
                    Unit = "Whole",
                    UnitPrice = 6.60,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 4,
                    ItemName = "Cucumber",
                    Unit = "1 whole",
                    UnitPrice = 1.90,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 5,
                    ItemName = "Red Potato Washed",
                    Unit = "1kg",
                    UnitPrice = 4.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 6,
                    ItemName = "Red Tipped Bananas",
                    Unit = "1kg",
                    UnitPrice = 4.90,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 7,
                    ItemName = "Red Onion",
                    Unit = "1kg",
                    UnitPrice = 3.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 8,
                    ItemName = "Carrots",
                    Unit = "1kg",
                    UnitPrice = 2.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 9,
                    ItemName = "Iceburg Lettuce",
                    Unit = "1",
                    UnitPrice = 2.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 10,
                    ItemName = "Helga's Wholemeal",
                    Unit = "1",
                    UnitPrice = 3.70,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 11,
                    ItemName = "Free Range Chicken",
                    Unit = "1kg",
                    UnitPrice = 7.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 12,
                    ItemName = "Manning Valley 6-pk",
                    Unit = "6 eggs",
                    UnitPrice = 3.60,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 13,
                    ItemName = "A2 Light Milk",
                    Unit = "1 litre",
                    UnitPrice = 2.90,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 14,
                    ItemName = "Chobani Strawberry Yoghurt",
                    Unit = "1",
                    UnitPrice = 1.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 15,
                    ItemName = "Lurpark Salted Blend",
                    Unit = "250g",
                    UnitPrice = 5.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 16,
                    ItemName = "Bega Farmers Tasty",
                    Unit = "250g",
                    UnitPrice = 4.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 17,
                    ItemName = "Babybel Mini",
                    Unit = "100g",
                    UnitPrice = 4.20,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 18,
                    ItemName = "Cobram EVOO",
                    Unit = "375ml",
                    UnitPrice = 8.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 19,
                    ItemName = "Heinz Tomato Soup",
                    Unit = "535g",
                    UnitPrice = 2.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 20,
                    ItemName = "John West Tuna can",
                    Unit = "95g",
                    UnitPrice = 1.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 21,
                    ItemName = "Cadbury Dairy Milk",
                    Unit = "200g",
                    UnitPrice = 5.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 22,
                    ItemName = "Coca Cola",
                    Unit = "2 litre",
                    UnitPrice = 2.85,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 23,
                    ItemName = "Smith's Original Share Pack Crisps",
                    Unit = "170g",
                    UnitPrice = 3.29,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 24,
                    ItemName = "Birds Eye Fish Fingers",
                    Unit = "375g",
                    UnitPrice = 4.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 25,
                    ItemName = "Berri Orange Juice",
                    Unit = "2 litre",
                    UnitPrice = 6.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 26,
                    ItemName = "Vegemite",
                    Unit = "380g",
                    UnitPrice = 6.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 27,
                    ItemName = "Cheddar Shapes",
                    Unit = "175g",
                    UnitPrice = 2.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 28,
                    ItemName = "Colgate Total ToothPaste",
                    Unit = "110g",
                    UnitPrice = 3.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 29,
                    ItemName = "Milo Chocolate Malt",
                    Unit = "200g",
                    UnitPrice = 4.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 30,
                    ItemName = "Weet Bix Saniatarium Organic",
                    Unit = "750g",
                    UnitPrice = 5.33,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 31,
                    ItemName = "Lindt Excellence 70% Cocoa Block",
                    Unit = "100g",
                    UnitPrice = 4.25,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 32,
                    ItemName = "Original Tim Tams Chocolate",
                    Unit = "200g",
                    UnitPrice = 3.65,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 33,
                    ItemName = "Philadelphia Original Cream Cheese",
                    Unit = "250g",
                    UnitPrice = 4.30,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 34,
                    ItemName = "Moccona Classic Instant Medium Roast",
                    Unit = "100g",
                    UnitPrice = 6.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 35,
                    ItemName = "Capilano Sqeezable Honey",
                    Unit = "500g",
                    UnitPrice = 7.35,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 36,
                    ItemName = "Nutella Jar",
                    Unit = "400g",
                    UnitPrice = 4.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 37,
                    ItemName = "Arnott's Scotch Finger",
                    Unit = "250g",
                    UnitPrice = 2.85,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 38,
                    ItemName = "South Cape Greek Feta",
                    Unit = "200g",
                    UnitPrice = 5.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 39,
                    ItemName = "Sacla Pasta Tomato Basil Sauce",
                    Unit = "420g",
                    UnitPrice = 4.50,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 40,
                    ItemName = "Primo English Ham",
                    Unit = "100g",
                    UnitPrice = 3.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 41,
                    ItemName = "Primo Short Cut Rindless Bacon",
                    Unit = "175g",
                    UnitPrice = 5.00,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 42,
                    ItemName = "Golden Circle Pinapple Pieces in natural juice",
                    Unit = "440g",
                    UnitPrice = 3.25,
                },

                new SamsWareHouseItem
                {
                    SamsWareHouseItemId = 43,
                    ItemName = "San Renmo Linguine Pasta No 1",
                    Unit = "500g",
                    UnitPrice = 1.95,
                });

            modelBuilder.Entity<ShoppingListItem>()
                .HasOne(fli => fli.ShoppingList)
                .WithMany(fl => fl.ShoppingListItems)
                .HasForeignKey(fli => fli.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingListItem>()
                .HasOne(fli => fli.SamsWareHouseItem)
                .WithMany(j => j.ShoppingListItems)
                .HasForeignKey(fli => fli.SamsWareHouseItemId)
                .OnDelete(DeleteBehavior.Cascade);

        }



    }

}
