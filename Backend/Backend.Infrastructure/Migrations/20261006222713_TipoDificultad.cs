using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TipoDificultad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoDificultad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDificultad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntervencionTipoDificultad",
                columns: table => new
                {
                    IntervencionId = table.Column<int>(type: "int", nullable: false),
                    TipoDificultadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntervencionTipoDificultad", x => new { x.IntervencionId, x.TipoDificultadId });
                    table.ForeignKey(
                        name: "FK_IntervencionTipoDificultad_Intervencion_IntervencionId",
                        column: x => x.IntervencionId,
                        principalTable: "Intervencion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntervencionTipoDificultad_TipoDificultad_TipoDificultadId",
                        column: x => x.TipoDificultadId,
                        principalTable: "TipoDificultad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TipoDificultad",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Dificultades de atención" },
                    { 2, "Dificultades en lectoescritura" },
                    { 3, "Dificultades en cálculo" },
                    { 4, "Dificultades en memoria" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntervencionTipoDificultad_TipoDificultadId",
                table: "IntervencionTipoDificultad",
                column: "TipoDificultadId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoDificultad_Nombre",
                table: "TipoDificultad",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntervencionTipoDificultad");

            migrationBuilder.DropTable(
                name: "TipoDificultad");
        }
    }
}
