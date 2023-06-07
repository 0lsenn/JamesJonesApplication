using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JamesJonesDbs2.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SamsWareHouseItems",
                columns: table => new
                {
                    SamsWareHouseItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamsWareHouseItems", x => x.SamsWareHouseItemId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_AppUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SamsWareHouseItemId = table.Column<int>(type: "int", nullable: false),
                    ShoppingListId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListItems_SamsWareHouseItems_SamsWareHouseItemId",
                        column: x => x.SamsWareHouseItemId,
                        principalTable: "SamsWareHouseItems",
                        principalColumn: "SamsWareHouseItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListItems_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SamsWareHouseItems",
                columns: new[] { "SamsWareHouseItemId", "ItemName", "Unit", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Granny Smith Apples", "1kg", 5.5 },
                    { 2, "Fresh Tomatoes", "500g", 5.9000000000000004 },
                    { 3, "Watermelon", "Whole", 6.5999999999999996 },
                    { 4, "Cucumber", "1 whole", 1.8999999999999999 },
                    { 5, "Red Potato Washed", "1kg", 4.0 },
                    { 6, "Red Tipped Bananas", "1kg", 4.9000000000000004 },
                    { 7, "Red Onion", "1kg", 3.5 },
                    { 8, "Carrots", "1kg", 2.0 },
                    { 9, "Iceburg Lettuce", "1", 2.5 },
                    { 10, "Helga's Wholemeal", "1", 3.7000000000000002 },
                    { 11, "Free Range Chicken", "1kg", 7.5 },
                    { 12, "Manning Valley 6-pk", "6 eggs", 3.6000000000000001 },
                    { 13, "A2 Light Milk", "1 litre", 2.8999999999999999 },
                    { 14, "Chobani Strawberry Yoghurt", "1", 1.5 },
                    { 15, "Lurpark Salted Blend", "250g", 5.0 },
                    { 16, "Bega Farmers Tasty", "250g", 4.0 },
                    { 17, "Babybel Mini", "100g", 4.2000000000000002 },
                    { 18, "Cobram EVOO", "375ml", 8.0 },
                    { 19, "Heinz Tomato Soup", "535g", 2.5 },
                    { 20, "John West Tuna can", "95g", 1.5 },
                    { 21, "Cadbury Dairy Milk", "200g", 5.0 },
                    { 22, "Coca Cola", "2 litre", 2.8500000000000001 },
                    { 23, "Smith's Original Share Pack Crisps", "170g", 3.29 },
                    { 24, "Birds Eye Fish Fingers", "375g", 4.5 },
                    { 25, "Berri Orange Juice", "2 litre", 6.0 },
                    { 26, "Vegemite", "380g", 6.0 },
                    { 27, "Cheddar Shapes", "175g", 2.0 },
                    { 28, "Colgate Total ToothPaste", "110g", 3.5 },
                    { 29, "Milo Chocolate Malt", "200g", 4.0 },
                    { 30, "Weet Bix Saniatarium Organic", "750g", 5.3300000000000001 },
                    { 31, "Lindt Excellence 70% Cocoa Block", "100g", 4.25 },
                    { 32, "Original Tim Tams Chocolate", "200g", 3.6499999999999999 },
                    { 33, "Philadelphia Original Cream Cheese", "250g", 4.2999999999999998 },
                    { 34, "Moccona Classic Instant Medium Roast", "100g", 6.0 },
                    { 35, "Capilano Sqeezable Honey", "500g", 7.3499999999999996 },
                    { 36, "Nutella Jar", "400g", 4.0 },
                    { 37, "Arnott's Scotch Finger", "250g", 2.8500000000000001 },
                    { 38, "South Cape Greek Feta", "200g", 5.0 },
                    { 39, "Sacla Pasta Tomato Basil Sauce", "420g", 4.5 },
                    { 40, "Primo English Ham", "100g", 3.0 },
                    { 41, "Primo Short Cut Rindless Bacon", "175g", 5.0 },
                    { 42, "Golden Circle Pinapple Pieces in natural juice", "440g", 3.25 }
                });

            migrationBuilder.InsertData(
                table: "SamsWareHouseItems",
                columns: new[] { "SamsWareHouseItemId", "ItemName", "Unit", "UnitPrice" },
                values: new object[] { 43, "San Renmo Linguine Pasta No 1", "500g", 1.95 });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItems_SamsWareHouseItemId",
                table: "ShoppingListItems",
                column: "SamsWareHouseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItems_ShoppingListId",
                table: "ShoppingListItems",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_AppUserID",
                table: "ShoppingLists",
                column: "AppUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListItems");

            migrationBuilder.DropTable(
                name: "SamsWareHouseItems");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
