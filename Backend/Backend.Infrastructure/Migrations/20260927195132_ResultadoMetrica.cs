using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResultadoMetrica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Unidad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TipoAgregacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    EjercicioId = table.Column<int>(type: "int", nullable: false),
                    Aciertos = table.Column<int>(type: "int", nullable: false),
                    Errores = table.Column<int>(type: "int", nullable: false),
                    TiempoSegundos = table.Column<int>(type: "int", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultado", x => new { x.SesionId, x.EjercicioId });
                    table.ForeignKey(
                        name: "FK_Resultado_SesionEjercicio_SesionId_EjercicioId",
                        columns: x => new { x.SesionId, x.EjercicioId },
                        principalTable: "SesionEjercicio",
                        principalColumns: new[] { "SesionId", "EjercicioId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResultadoMetrica",
                columns: table => new
                {
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    EjercicioId = table.Column<int>(type: "int", nullable: false),
                    MetricaId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoMetrica", x => new { x.SesionId, x.EjercicioId, x.MetricaId });
                    table.ForeignKey(
                        name: "FK_ResultadoMetrica_Metrica_MetricaId",
                        column: x => x.MetricaId,
                        principalTable: "Metrica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResultadoMetrica_Resultado_SesionId_EjercicioId",
                        columns: x => new { x.SesionId, x.EjercicioId },
                        principalTable: "Resultado",
                        principalColumns: new[] { "SesionId", "EjercicioId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metrica_Nombre",
                table: "Metrica",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoMetrica_MetricaId",
                table: "ResultadoMetrica",
                column: "MetricaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultadoMetrica");

            migrationBuilder.DropTable(
                name: "Metrica");

            migrationBuilder.DropTable(
                name: "Resultado");
        }
    }
}
