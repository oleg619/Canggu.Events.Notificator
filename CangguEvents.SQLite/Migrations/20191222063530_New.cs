using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CangguEvents.SQLite.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instruments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    BaseCurrency = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    MaxQuantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinQuantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PricePrecision = table.Column<long>(type: "INTEGER", nullable: false),
                    PriceTick = table.Column<decimal>(type: "TEXT", nullable: false),
                    QuantityPrecision = table.Column<long>(type: "INTEGER", nullable: false),
                    QuantityTick = table.Column<decimal>(type: "TEXT", nullable: false),
                    QuoteCurrency = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });
        }
    }
}
