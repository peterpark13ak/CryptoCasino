using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCasino.DataContext.Migrations
{
    public partial class AddingDefaultAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b7552f15-b934-4b99-b6b1-ef427bda427d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f1ec6ace-81e0-4e79-9c35-293103ce5177");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Alias", "ConcurrencyStamp", "CreatedOn", "DateOfBirth", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "Locked", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8dd8e5b3-02f5-4504-a188-fed9386e26c8", 0, "Boss", "759d34a5-3668-43f3-8bc3-57a3e2b1949a", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", true, false, false, false, null, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEP8nOVfDCrHB1FfM+P3Zuvz9FuU/skpBWBzxdR/HmN5EDoP60zHJTmUoi7gecKaCbQ==", "+359359", true, "d9e28cf2-36d2-4ecb-bc09-dc773f343fcd", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8dd8e5b3-02f5-4504-a188-fed9386e26c8", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8dd8e5b3-02f5-4504-a188-fed9386e26c8", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8dd8e5b3-02f5-4504-a188-fed9386e26c8", "759d34a5-3668-43f3-8bc3-57a3e2b1949a" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "24e34c1c-8c6d-4b72-bb4f-7e6ab1f8b62d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2664ff07-05cc-4711-96fe-dbce2afa4698");
        }
    }
}
