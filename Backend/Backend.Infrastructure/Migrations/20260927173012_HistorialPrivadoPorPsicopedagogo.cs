using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
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

            migrationBuilder.AddColumn<int>(
                name: "PsicopedagogoId",
                table: "Sesion",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            // Conserva los datos existentes. FechaInicio arranca en la fecha de alta del paciente para
            // todos los vínculos. La intervención del paciente pasa al vínculo más antiguo (menor
            // PsicopedagogoId); si el paciente tenía más de un psicopedagogo, los demás vínculos arrancan
            // con una intervención propia en blanco, porque el historial ahora es privado de cada uno.
            migrationBuilder.Sql(
                "UPDATE pp SET pp.FechaInicio = p.FechaAlta " +
                "FROM PsicopedagogoPaciente pp INNER JOIN Paciente p ON p.PersonaId = pp.PacienteId");

            migrationBuilder.Sql(@"
                ;WITH Ganador AS (
                    SELECT PacienteId, PsicopedagogoId,
                           ROW_NUMBER() OVER (PARTITION BY PacienteId ORDER BY PsicopedagogoId) AS rn
                    FROM PsicopedagogoPaciente
                )
                UPDATE pp SET pp.IntervencionId = p.IntervencionId
                FROM PsicopedagogoPaciente pp
                INNER JOIN Ganador g ON g.PacienteId = pp.PacienteId AND g.PsicopedagogoId = pp.PsicopedagogoId AND g.rn = 1
                INNER JOIN Paciente p ON p.PersonaId = pp.PacienteId;

                DECLARE @PacienteId int, @PsicopedagogoId int, @NuevaIntervencionId int;
                DECLARE perdedores CURSOR LOCAL FAST_FORWARD FOR
                    SELECT pp.PacienteId, pp.PsicopedagogoId
                    FROM PsicopedagogoPaciente pp
                    WHERE EXISTS (
                        SELECT 1 FROM PsicopedagogoPaciente otro
                        WHERE otro.PacienteId = pp.PacienteId AND otro.PsicopedagogoId < pp.PsicopedagogoId
                    );

                OPEN perdedores;
                FETCH NEXT FROM perdedores INTO @PacienteId, @PsicopedagogoId;
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    INSERT INTO Intervencion (objetivo, observaciones) VALUES (NULL, NULL);
                    SET @NuevaIntervencionId = SCOPE_IDENTITY();

                    UPDATE PsicopedagogoPaciente SET IntervencionId = @NuevaIntervencionId
                    WHERE PacienteId = @PacienteId AND PsicopedagogoId = @PsicopedagogoId;

                    FETCH NEXT FROM perdedores INTO @PacienteId, @PsicopedagogoId;
                END
                CLOSE perdedores;
                DEALLOCATE perdedores;");

            // Las sesiones quedan del psicopedagogo con el vínculo más antiguo para ese paciente.
            migrationBuilder.Sql(
                "UPDATE s SET s.PsicopedagogoId = " +
                "(SELECT TOP 1 pp.PsicopedagogoId FROM PsicopedagogoPaciente pp WHERE pp.PacienteId = s.PacienteId ORDER BY pp.PsicopedagogoId) " +
                "FROM Sesion s");

            migrationBuilder.CreateTable(
                name: "SesionEjercicio",
                columns: table => new
                {
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    EjercicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionEjercicio", x => new { x.SesionId, x.EjercicioId });
                    table.ForeignKey(
                        name: "FK_SesionEjercicio_Ejercicio_EjercicioId",
                        column: x => x.EjercicioId,
                        principalTable: "Ejercicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SesionEjercicio_Sesion_SesionId",
                        column: x => x.SesionId,
                        principalTable: "Sesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_PsicopedagogoId_PacienteId",
                table: "Sesion",
                columns: new[] { "PsicopedagogoId", "PacienteId" });

            migrationBuilder.CreateIndex(
                name: "IX_PsicopedagogoPaciente_IntervencionId",
                table: "PsicopedagogoPaciente",
                column: "IntervencionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SesionEjercicio_EjercicioId",
                table: "SesionEjercicio",
                column: "EjercicioId");

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

            migrationBuilder.DropTable(
                name: "SesionEjercicio");

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
