using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCasino.DataContext.Migrations
{
    public partial class DefaultRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transcations");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    NormalisedAmount = table.Column<double>(nullable: false),
                    OriginalAmount = table.Column<double>(nullable: false),
                    CardId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_BankCards_CardId",
                        column: x => x.CardId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "24e34c1c-8c6d-4b72-bb4f-7e6ab1f8b62d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "2664ff07-05cc-4711-96fe-dbce2afa4698", "Player", "PLAYER" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CardId",
                table: "Transactions",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1", "24e34c1c-8c6d-4b72-bb4f-7e6ab1f8b62d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2", "2664ff07-05cc-4711-96fe-dbce2afa4698" });

            migrationBuilder.CreateTable(
                name: "Transcations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CardId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    NormalisedAmount = table.Column<double>(nullable: false),
                    OriginalAmount = table.Column<double>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcations_BankCards_CardId",
                        column: x => x.CardId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transcations_TransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transcations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transcations_CardId",
                table: "Transcations",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcations_TransactionTypeId",
                table: "Transcations",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcations_UserId",
                table: "Transcations",
                column: "UserId");
        }
    }
}
