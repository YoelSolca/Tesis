using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PsicopedagogoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PsicopedagogoPaciente",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    PsicopedagogoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsicopedagogoPaciente", x => new { x.PsicopedagogoId, x.PacienteId });
                    table.ForeignKey(
                        name: "FK_PsicopedagogoPaciente_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsicopedagogoPaciente_Psicopedagogo_PsicopedagogoId",
                        column: x => x.PsicopedagogoId,
                        principalTable: "Psicopedagogo",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PsicopedagogoPaciente_PacienteId",
                table: "PsicopedagogoPaciente",
                column: "PacienteId");

            // Conserva las asignaciones existentes (relación 1 a muchos anterior) antes de borrar la columna.
            migrationBuilder.Sql(
                "INSERT INTO PsicopedagogoPaciente (PacienteId, PsicopedagogoId) " +
                "SELECT PersonaId, PsicopedagogoId FROM Paciente");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PsicopedagogoId",
                table: "Paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Al volver a 1 a muchos solo cabe un psicopedagogo por paciente: se conserva uno.
            migrationBuilder.Sql(
                "UPDATE p SET p.PsicopedagogoId = " +
                "(SELECT TOP 1 pp.PsicopedagogoId FROM PsicopedagogoPaciente pp WHERE pp.PacienteId = p.PersonaId) " +
                "FROM Paciente p WHERE EXISTS (SELECT 1 FROM PsicopedagogoPaciente pp WHERE pp.PacienteId = p.PersonaId)");

            migrationBuilder.DropTable(
                name: "PsicopedagogoPaciente");

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
    }
}
