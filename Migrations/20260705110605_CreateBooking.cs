using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class CreateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LiqPayPaymentId",
                table: "Bookings",
                newName: "LiqPayTransactionId");

            migrationBuilder.AddColumn<string>(
                name: "BookingNumber",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardLast4",
                table: "Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CardLast4",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaidAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "LiqPayTransactionId",
                table: "Bookings",
                newName: "LiqPayPaymentId");
        }
    }
}
