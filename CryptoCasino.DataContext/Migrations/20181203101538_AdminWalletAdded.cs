using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCasino.DataContext.Migrations
{
    public partial class AdminWalletAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "043b1fa8-8f8d-4b37-bc88-d1c1f1fc25ee", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "043b1fa8-8f8d-4b37-bc88-d1c1f1fc25ee", "4f2f16ab-9bfb-4a5b-b2a5-d40ea97152e6" });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    ExchangeRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "1e796ba7-f4ec-4ad9-8d47-bf21e43ae249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "37ee0141-d1c7-45d0-ba5b-8f7b9c358a67");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Alias", "ConcurrencyStamp", "CreatedOn", "DateOfBirth", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "Locked", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "abba12b6-5226-4067-8011-08231c0fe823", 0, "Boss", "2e32c6ce-01c7-4bad-9baa-e202bf4eca02", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", true, false, false, false, null, new DateTime(2018, 12, 3, 12, 15, 38, 473, DateTimeKind.Local), "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAECNfFBsMnAg4BuFnPp4cYNuGXXsJKfMyw8lDNGG6ULWFMikEyw3TuMGKQEoSBpV/yQ==", "+359359", true, "c807b4bc-f906-4e05-8aae-fd8cda0ccaf1", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "abba12b6-5226-4067-8011-08231c0fe823", "1" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "CreatedOn", "CurrencyId", "DeletedOn", "Deposits", "DisplayBalance", "IsDeleted", "ModifiedOn", "NormalisedBalance", "Stakes", "UserId", "Wins" },
                values: new object[] { "admin-wallet", null, 1, null, 0.0, 0.0, false, null, 0.0, 0.0, "abba12b6-5226-4067-8011-08231c0fe823", 0.0 });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "abba12b6-5226-4067-8011-08231c0fe823", "1" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: "admin-wallet");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "abba12b6-5226-4067-8011-08231c0fe823", "2e32c6ce-01c7-4bad-9baa-e202bf4eca02" });

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
    }
}
