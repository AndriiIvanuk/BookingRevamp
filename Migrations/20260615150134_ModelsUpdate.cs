using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirConditioner",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Bedclothes",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Elevator",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FastWifi",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Generator",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Heating",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Iron",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Safe",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "TV",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Wardrobe",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CVV",
                table: "PartnerApplications");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "PartnerApplications");

            migrationBuilder.CreateTable(
                name: "PropertyAmenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    FastWifi = table.Column<bool>(type: "boolean", nullable: false),
                    AirConditioner = table.Column<bool>(type: "boolean", nullable: false),
                    Heating = table.Column<bool>(type: "boolean", nullable: false),
                    TV = table.Column<bool>(type: "boolean", nullable: false),
                    Generator = table.Column<bool>(type: "boolean", nullable: false),
                    Elevator = table.Column<bool>(type: "boolean", nullable: false),
                    Wardrobe = table.Column<bool>(type: "boolean", nullable: false),
                    Bedclothes = table.Column<bool>(type: "boolean", nullable: false),
                    Iron = table.Column<bool>(type: "boolean", nullable: false),
                    Safe = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAmenities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_PropertyId",
                table: "PropertyAmenities",
                column: "PropertyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyAmenities");

            migrationBuilder.AddColumn<bool>(
                name: "AirConditioner",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Bedclothes",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Elevator",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FastWifi",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Generator",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Heating",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Iron",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Safe",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TV",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wardrobe",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CVV",
                table: "PartnerApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpiryDate",
                table: "PartnerApplications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
