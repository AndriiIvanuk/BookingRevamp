using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingRevamp.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreAmenity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Category", "Icon", "Name", "Size" },
                values: new object[,]
                {
                    { 11, "Кухня та харчування", "kitchen-set.png", "Повністю обладнана кухня", "l" },
                    { 12, "Кухня та харчування", "freezer.png", "Холодильник", "m" },
                    { 13, "Кухня та харчування", "stove.png", "Плита", "m" },
                    { 14, "Кухня та харчування", "oven.png", "Духова піч", "m" },
                    { 15, "Кухня та харчування", "coffee-maker.png", "Кавоварка", "m" },
                    { 16, "Кухня та харчування", "kettle.png", "Електрочайник", "m" },
                    { 17, "Кухня та харчування", "microwave.png", "Мікрохвильова піч", "l" },
                    { 18, "Ванна кімната та прання", "separate-bath.png", "Власна ванна кімната", "l" },
                    { 19, "Ванна кімната та прання", "shower.png", "Душ", "s" },
                    { 20, "Ванна кімната та прання", "bathtub.png", "Ванна", "m" },
                    { 21, "Ванна кімната та прання", "washing-machine.png", "Пральна машина", "l" },
                    { 22, "Ванна кімната та прання", "towels.png", "Рушники", "m" },
                    { 23, "Ванна кімната та прання", "cosmetics.png", "Косметичні засоби", "l" },
                    { 24, "Територія та вигляд", "balcony.png", "Балкон", "m" },
                    { 25, "Територія та вигляд", "terrace.png", "Тераса", "m" },
                    { 26, "Територія та вигляд", "sea-view.png", "Вид на море", "m" },
                    { 27, "Територія та вигляд", "mountain-view.png", "Вид на гори", "m" },
                    { 28, "Територія та вигляд", "closed-area.png", "Закрита тереторія", "m" },
                    { 29, "Територія та вигляд", "garden.png", "Сад", "s" },
                    { 30, "Для дітей", "baby-bed.png", "Дитяче ліжечко", "m" },
                    { 31, "Для дітей", "play-area.png", "Ігрова зона", "m" },
                    { 32, "Додаткові можливості", "cleaning.png", "Прибирання", "m" },
                    { 33, "Додаткові можливості", "breakfast.png", "Сніданок включено", "l" },
                    { 34, "Безпека", "security-camera.png", "Відеоспостереження", "l" },
                    { 35, "Безпека", "bunker.png", "Укриття", "m" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 35);
        }
    }
}
