using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CangguEvents.SQLite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BaseCurrency = table.Column<string>(nullable: true),
                    QuoteCurrency = table.Column<string>(nullable: true),
                    MaxQuantity = table.Column<decimal>(nullable: false),
                    MinQuantity = table.Column<decimal>(nullable: false),
                    MaxValue = table.Column<decimal>(nullable: false),
                    MinValue = table.Column<decimal>(nullable: false),
                    PricePrecision = table.Column<long>(nullable: false),
                    QuantityPrecision = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    PriceTick = table.Column<decimal>(nullable: false),
                    QuantityTick = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    GoogleUrl = table.Column<string>(nullable: true),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Image = table.Column<byte[]>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_LocationEntity_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayOfWeekEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    EventEntityId = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeekEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayOfWeekEntity_Events_EventEntityId",
                        column: x => x.EventEntityId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekEntity_EventEntityId",
                table: "DayOfWeekEntity",
                column: "EventEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOfWeekEntity");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "LocationEntity");
        }
    }
}
