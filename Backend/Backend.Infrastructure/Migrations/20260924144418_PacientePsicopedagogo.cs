using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PacientePsicopedagogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PsicopedagogoId",
                table: "Paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PsicopedagogoId",
                table: "Paciente",
                column: "PsicopedagogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Psicopedagogo_PsicopedagogoId",
                table: "Paciente",
                column: "PsicopedagogoId",
                principalTable: "Psicopedagogo",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Psicopedagogo_PsicopedagogoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_PsicopedagogoId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "PsicopedagogoId",
                table: "Paciente");
        }
    }
}
