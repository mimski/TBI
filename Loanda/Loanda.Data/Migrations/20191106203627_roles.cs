using Microsoft.EntityFrameworkCore.Migrations;

namespace Loanda.Data.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45a3335a-44de-44f7-b77c-bfa7d3c10a7c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45a3335a-44de-44f7-b77c-bfa7d3c10a7c", 0, "9d737330-f5d9-410c-a9e1-f8aec11903f9", null, null, "shaban9726@gmail.com", false, false, false, null, null, "SHABAN9726@GMAIL.COM", "SHABAN9726@GMAIL.COM", "AQAAAAEAACcQAAAAEFlZ3okaz7hZUfZV1qgvOLac2WRWCxSNuhzwaB9Of93MvQncQQYZj2fb6bLSH4VFRw==", null, false, "FJBKMINGFQAZNGSMZAIYUUQEVK4T74RU", false, "shaban9726@gmail.com" });
        }
    }
}
