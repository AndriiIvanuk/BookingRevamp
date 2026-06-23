using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class PropertyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PropertyType = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Object = table.Column<string>(type: "text", nullable: false),
                    MaxGuests = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Rules = table.Column<string>(type: "text", nullable: false),
                    AllowPets = table.Column<bool>(type: "boolean", nullable: false),
                    AllowChildren = table.Column<bool>(type: "boolean", nullable: false),
                    FastWifi = table.Column<bool>(type: "boolean", nullable: false),
                    AirConditioner = table.Column<bool>(type: "boolean", nullable: false),
                    Heating = table.Column<bool>(type: "boolean", nullable: false),
                    TV = table.Column<bool>(type: "boolean", nullable: false),
                    Generator = table.Column<bool>(type: "boolean", nullable: false),
                    Elevator = table.Column<bool>(type: "boolean", nullable: false),
                    Wardrobe = table.Column<bool>(type: "boolean", nullable: false),
                    Bedclothes = table.Column<bool>(type: "boolean", nullable: false),
                    Iron = table.Column<bool>(type: "boolean", nullable: false),
                    Safe = table.Column<bool>(type: "boolean", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    PropertyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImages",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
