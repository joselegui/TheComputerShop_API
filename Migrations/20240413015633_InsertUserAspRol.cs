using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheComputerShop.Migrations
{
    /// <inheritdoc />
    public partial class InsertUserAspRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "097409b0-9c63-46c5-b62c-7b47059b9370", null, "UserAspRol", "Super Administrator", "SUPER ADMINISTRATOR" },
                    { "8347125b-b147-4b20-bfcb-1d5a3b55da47", null, "UserAspRol", "User", "USER" },
                    { "84a2de5a-1f2b-43ab-9450-d53738848f7f", null, "UserAspRol", "Administrator", "ADMINISTRATOR" },
                    { "bed9b6d3-96d9-4691-b331-eead5b2265e4", null, "UserAspRol", "The seller", "THE SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "097409b0-9c63-46c5-b62c-7b47059b9370");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8347125b-b147-4b20-bfcb-1d5a3b55da47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84a2de5a-1f2b-43ab-9450-d53738848f7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bed9b6d3-96d9-4691-b331-eead5b2265e4");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
