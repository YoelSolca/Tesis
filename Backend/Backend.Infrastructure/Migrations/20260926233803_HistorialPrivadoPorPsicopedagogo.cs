using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <summary>
    /// Mueve la intervención y las sesiones del paciente al vínculo psicopedagogo-paciente (historial privado)
    /// y agrega FechaInicio/FechaFin al vínculo. Los datos existentes se conservan: cada paciente tenía un solo
    /// psicopedagogo antes de esta migración, así que su intervención y sus sesiones pasan a ese vínculo.
    /// </summary>
    public partial class HistorialPrivadoPorPsicopedagogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_Paciente_PacienteId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_PacienteId",
                table: "Sesion");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "PsicopedagogoPaciente",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "PsicopedagogoPaciente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IntervencionId",
                table: "PsicopedagogoPaciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsicopedagogoId",
                table: "Sesion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // La intervención del paciente pasa a su vínculo, y la atención arranca cuando se dio de alta.
            migrationBuilder.Sql(
                "UPDATE pp SET pp.IntervencionId = p.IntervencionId, pp.FechaInicio = p.FechaAlta " +
                "FROM PsicopedagogoPaciente pp INNER JOIN Paciente p ON p.PersonaId = pp.PacienteId");

            // Las sesiones pasan al (único) psicopedagogo que tenía el paciente.
            migrationBuilder.Sql(
                "UPDATE s SET s.PsicopedagogoId = " +
                "(SELECT TOP 1 pp.PsicopedagogoId FROM PsicopedagogoPaciente pp WHERE pp.PacienteId = s.PacienteId ORDER BY pp.FechaInicio) " +
                "FROM Sesion s");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_PsicopedagogoId_PacienteId",
                table: "Sesion",
                columns: new[] { "PsicopedagogoId", "PacienteId" });

            migrationBuilder.CreateIndex(
                name: "IX_PsicopedagogoPaciente_IntervencionId",
                table: "PsicopedagogoPaciente",
                column: "IntervencionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PsicopedagogoPaciente_Intervencion_IntervencionId",
                table: "PsicopedagogoPaciente",
                column: "IntervencionId",
                principalTable: "Intervencion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sesion_PsicopedagogoPaciente_PsicopedagogoId_PacienteId",
                table: "Sesion",
                columns: new[] { "PsicopedagogoId", "PacienteId" },
                principalTable: "PsicopedagogoPaciente",
                principalColumns: new[] { "PsicopedagogoId", "PacienteId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Intervencion_IntervencionId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_IntervencionId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "IntervencionId",
                table: "Paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervencionId",
                table: "Paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Al volver a un historial compartido por paciente se conserva la intervención de un solo vínculo.
            migrationBuilder.Sql(
                "UPDATE p SET p.IntervencionId = " +
                "(SELECT TOP 1 pp.IntervencionId FROM PsicopedagogoPaciente pp WHERE pp.PacienteId = p.PersonaId ORDER BY pp.FechaInicio) " +
                "FROM Paciente p WHERE EXISTS (SELECT 1 FROM PsicopedagogoPaciente pp WHERE pp.PacienteId = p.PersonaId)");

            migrationBuilder.DropForeignKey(
                name: "FK_PsicopedagogoPaciente_Intervencion_IntervencionId",
                table: "PsicopedagogoPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Sesion_PsicopedagogoPaciente_PsicopedagogoId_PacienteId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_Sesion_PsicopedagogoId_PacienteId",
                table: "Sesion");

            migrationBuilder.DropIndex(
                name: "IX_PsicopedagogoPaciente_IntervencionId",
                table: "PsicopedagogoPaciente");

            migrationBuilder.DropColumn(
                name: "PsicopedagogoId",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "PsicopedagogoPaciente");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "PsicopedagogoPaciente");

            migrationBuilder.DropColumn(
                name: "IntervencionId",
                table: "PsicopedagogoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_PacienteId",
                table: "Sesion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_IntervencionId",
                table: "Paciente",
                column: "IntervencionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Intervencion_IntervencionId",
                table: "Paciente",
                column: "IntervencionId",
                principalTable: "Intervencion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sesion_Paciente_PacienteId",
                table: "Sesion",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
