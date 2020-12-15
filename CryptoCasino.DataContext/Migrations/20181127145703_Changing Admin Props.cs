using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCasino.DataContext.Migrations
{
    public partial class ChangingAdminProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "1d5ceff1-46f6-487d-b123-cbbb695b747d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9a79bf90-d06c-4edd-827a-096178dc7fef");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Alias", "ConcurrencyStamp", "CreatedOn", "DateOfBirth", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "Locked", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "043b1fa8-8f8d-4b37-bc88-d1c1f1fc25ee", 0, "Boss", "4f2f16ab-9bfb-4a5b-b2a5-d40ea97152e6", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", true, false, false, false, null, new DateTime(2018, 11, 27, 16, 57, 1, 139, DateTimeKind.Local), "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEBlbzKsNuicT5C5fpJAUN7SzjS6QC44L1Sh7jMH+YZru37ec3FcA+z4WI4bnCnRbwQ==", "+359359", true, "8461492d-ebdf-413a-a59f-e6976bd014f5", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "043b1fa8-8f8d-4b37-bc88-d1c1f1fc25ee", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "043b1fa8-8f8d-4b37-bc88-d1c1f1fc25ee", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "043b1fa8-8f8d-4b37-bc88-d1c1f1fc25ee", "4f2f16ab-9bfb-4a5b-b2a5-d40ea97152e6" });

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
    }
}
