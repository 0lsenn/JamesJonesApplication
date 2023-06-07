using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JamesJonesDbs2.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "EmailAddress", "PasswordHash", "Role" },
                values: new object[] { 1, "sean@sean", "$2a$11$1gEP4pCF0zWB118NxJ8pD./k9qtEwV5kGBYXC8jhfWlNh6wP9FCsK", "Admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "EmailAddress", "PasswordHash", "Role" },
                values: new object[] { 2, "kyle@kyle", "$2a$11$XhsjDGmqOyIWjuKdpJeZm.ysQ7b2KSheGSQNWG7jjMalUcIyAF/Vq", "Customer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AppUsers");
        }
    }
}
