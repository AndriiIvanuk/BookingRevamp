using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class AmenityCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Category", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Основні", "wifi.png", "Швидкісний WI-FI" },
                    { 2, "Основні", "air-conditioner.png", "Кондиціонер" },
                    { 3, "Основні", "heating.png", "Опалення" },
                    { 4, "Основні", "tv.png", "Телевізор" },
                    { 5, "Основні", "generator.png", "Генератор" },
                    { 6, "Основні", "elevator.png", "Ліфт" },
                    { 7, "Спальня", "wardrobe.png", "Шафа або гардероб" },
                    { 8, "Спальня", "bedclothes.png", "Постільна білизна" },
                    { 9, "Спальня", "iron.png", "Праска" },
                    { 10, "Спальня", "safe.png", "Сейф" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
