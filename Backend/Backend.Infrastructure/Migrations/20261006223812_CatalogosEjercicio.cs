using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CatalogosEjercicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Metrica",
                columns: new[] { "Id", "Descripcion", "Nombre", "TipoAgregacion", "Unidad" },
                values: new object[,]
                {
                    { 1, "Cantidad de movimientos realizados", "movimientos", "Suma", null },
                    { 2, "Estímulos omitidos", "omisiones", "Suma", null },
                    { 3, "Nivel máximo alcanzado", "nivelAlcanzado", "Maximo", null }
                });

            migrationBuilder.InsertData(
                table: "TipoEjercicio",
                columns: new[] { "Id", "Nombre", "icono" },
                values: new object[,]
                {
                    { 1, "Atención", "atencion" },
                    { 2, "Memoria", "memoria" },
                    { 3, "Lectura", "lectura" },
                    { 4, "Escritura", "escritura" },
                    { 5, "Cálculo", "calculo" }
                });

            migrationBuilder.InsertData(
                table: "Ejercicio",
                columns: new[] { "Id", "Nombre", "TipoEjercicioId" },
                values: new object[,]
                {
                    { 1, "Encontrá la imagen igual", 1 },
                    { 2, "Sombras y animales", 1 },
                    { 3, "Clasificar colores", 1 },
                    { 4, "Memorama", 2 },
                    { 5, "Conectar palabras", 3 },
                    { 6, "Comprensión de texto", 3 },
                    { 7, "Dictado de palabras", 4 },
                    { 8, "Sumas y restas", 5 },
                    { 9, "Series numéricas", 5 },
                    { 10, "Desafío de números", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ejercicio",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Metrica",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Metrica",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Metrica",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoEjercicio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoEjercicio",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoEjercicio",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoEjercicio",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TipoEjercicio",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
