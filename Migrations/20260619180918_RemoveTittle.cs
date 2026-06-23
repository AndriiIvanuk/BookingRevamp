using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTittle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Properties",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
