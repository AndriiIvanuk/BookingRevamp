using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingGuestInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Bookings");
        }
    }
}
