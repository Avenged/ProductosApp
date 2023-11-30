using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductosApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColumnaIdEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Marcas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Categorias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_EstadoId",
                table: "Productos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_EstadoId",
                table: "Marcas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EstadoId",
                table: "Categorias",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Estados_EstadoId",
                table: "Categorias",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marcas_Estados_EstadoId",
                table: "Marcas",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Estados_EstadoId",
                table: "Productos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Estados_EstadoId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Marcas_Estados_EstadoId",
                table: "Marcas");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Estados_EstadoId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Productos_EstadoId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Marcas_EstadoId",
                table: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_EstadoId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Categorias");

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "MarcaId", "Name", "Precio", "Unidades" },
                values: new object[,]
                {
                    { -2, null, "iPhone 14 Pro Max", 1200m, 10 },
                    { -1, null, "Televisor Samsung", 100m, 10 }
                });
        }
    }
}
