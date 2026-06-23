using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class RemovePropertyOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Properties",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
