using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeRate.Migrations
{
    public partial class InitialDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrenciesExchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISO_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purchase = table.Column<float>(type: "real", nullable: false),
                    Sale = table.Column<float>(type: "real", nullable: false),
                    Today_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrenciesExchange", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrenciesExchange");
        }
    }
}
