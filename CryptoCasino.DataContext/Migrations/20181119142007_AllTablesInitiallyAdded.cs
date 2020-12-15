using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCasino.DataContext.Migrations
{
    public partial class AllTablesInitiallyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankCards",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CardNumber = table.Column<string>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    MoneyAdded = table.Column<double>(nullable: false),
                    MoneyRetrieved = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transcations",
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

            migrationBuilder.InsertData(
                table: "TransactionType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Win" },
                    { 2, "Stake" },
                    { 3, "Deposit" },
                    { 4, "Withdraw" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_UserId",
                table: "BankCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogs_UserId",
                table: "LoginLogs",
                column: "UserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "Transcations");

            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "TransactionType");
        }
    }
}
