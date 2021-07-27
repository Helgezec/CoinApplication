using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoinApplication.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DenominationValue = table.Column<int>(type: "int", nullable: false),
                    Acceptable = table.Column<bool>(type: "bit", nullable: false),
                    MaxAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeOperationItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    MoneyId = table.Column<int>(type: "int", nullable: false),
                    ExchangeOperationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeOperationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeOperationItem_ExchangeOperations_ExchangeOperationId",
                        column: x => x.ExchangeOperationId,
                        principalTable: "ExchangeOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeOperationItem_Monies_MoneyId",
                        column: x => x.MoneyId,
                        principalTable: "Monies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Monies",
                columns: new[] { "Id", "Acceptable", "Amount", "DenominationValue", "MaxAmount" },
                values: new object[,]
                {
                    { 1, false, 100, 50, 20 },
                    { 2, false, 100, 100, 10 },
                    { 3, true, 0, 5, 100 },
                    { 4, true, 0, 10, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOperationItem_ExchangeOperationId",
                table: "ExchangeOperationItem",
                column: "ExchangeOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOperationItem_MoneyId",
                table: "ExchangeOperationItem",
                column: "MoneyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeOperationItem");

            migrationBuilder.DropTable(
                name: "ExchangeOperations");

            migrationBuilder.DropTable(
                name: "Monies");
        }
    }
}
