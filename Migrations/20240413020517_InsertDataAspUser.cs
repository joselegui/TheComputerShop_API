using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheComputerShop.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataAspUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bc2be71f-a9ab-4c8f-a264-5bdfb27cabb0", 0, "88de4d91-431e-45e3-b59b-59c870bec032", "admin@test.com", true, false, null, "admin@test.com", "ADMIN@test.com", "ADMIN@test.com", "AQAAAAIAAYagAAAAECNF403oNaq42f9TusQOn65NiV3ZSGmMG9Gg4NOa/8Mwcjt49ThnH1ihQZ3X/o/+5g==", null, false, "91e00da4-e158-49c2-9e1a-ff0366bf7703", false, "admin@test.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc2be71f-a9ab-4c8f-a264-5bdfb27cabb0");
        }
    }
}
