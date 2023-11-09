using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductosApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregoDataFicticia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "MarcaId", "Name", "Precio", "Unidades" },
                values: new object[,]
                {
                    { -2, null, "iPhone 14 Pro Max", 1200m, 10 },
                    { -1, null, "Televisor Samsung", 100m, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
