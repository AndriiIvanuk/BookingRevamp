using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAmenities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyAmenities",
                table: "PropertyAmenities");

            migrationBuilder.DropIndex(
                name: "IX_PropertyAmenities_PropertyId",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "AirConditioner",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Bedclothes",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Elevator",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "FastWifi",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Generator",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Heating",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Iron",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Safe",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "TV",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "Wardrobe",
                table: "PropertyAmenities");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PropertyAmenities",
                newName: "AmenityId");

            migrationBuilder.AlterColumn<int>(
                name: "AmenityId",
                table: "PropertyAmenities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyAmenities",
                table: "PropertyAmenities",
                columns: new[] { "PropertyId", "AmenityId" });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_AmenityId",
                table: "PropertyAmenities",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAmenities_Amenities_AmenityId",
                table: "PropertyAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAmenities_Amenities_AmenityId",
                table: "PropertyAmenities");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyAmenities",
                table: "PropertyAmenities");

            migrationBuilder.DropIndex(
                name: "IX_PropertyAmenities_AmenityId",
                table: "PropertyAmenities");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "PropertyAmenities",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PropertyAmenities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "AirConditioner",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Bedclothes",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Elevator",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FastWifi",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Generator",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Heating",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Iron",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Safe",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TV",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wardrobe",
                table: "PropertyAmenities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyAmenities",
                table: "PropertyAmenities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_PropertyId",
                table: "PropertyAmenities",
                column: "PropertyId",
                unique: true);
        }
    }
}
