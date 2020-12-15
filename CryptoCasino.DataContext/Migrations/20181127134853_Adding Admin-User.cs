using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCasino.DataContext.Migrations
{
    public partial class AddingAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "fd2b2a3c-98bf-499c-8b8a-ce084d38829f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4ff56ac1-aee6-47f4-975a-e3edd0dd3758");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Alias", "ConcurrencyStamp", "CreatedOn", "DateOfBirth", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "Locked", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b5f076ac-15c0-4b78-9ffe-912a687fbec9", 0, "Boss", "7b703308-d123-42ef-b3be-8a4b9c851d7b", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", true, false, false, false, null, new DateTime(2018, 11, 27, 15, 48, 51, 921, DateTimeKind.Local), "ADMIN@MAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEH+KjrZFostz6LJb1EJ0GyOcidwi9j4GxsHQpUK5VYILH5oPzMe4/KNWJJh6QULmOQ==", "+359359", true, "9cf37ad9-d517-4db1-8cd2-1f9bade72e48", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "b5f076ac-15c0-4b78-9ffe-912a687fbec9", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "b5f076ac-15c0-4b78-9ffe-912a687fbec9", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b5f076ac-15c0-4b78-9ffe-912a687fbec9", "7b703308-d123-42ef-b3be-8a4b9c851d7b" });

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
    }
}
