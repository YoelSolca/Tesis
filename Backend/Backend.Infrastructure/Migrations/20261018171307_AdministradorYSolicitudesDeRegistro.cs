using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdministradorYSolicitudesDeRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Psicopedagogo_PsicopedagogoId",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "PsicopedagogoId",
                table: "Usuario",
                newName: "PersonaId");

            migrationBuilder.AddColumn<bool>(
                name: "AceptoTerminos",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Usuario",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Aprobado"); // las cuentas que ya existían quedan aprobadas

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAceptacionTerminos",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitud",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuario",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Psicopedagogo");

            // Hasta ahora todo usuario era un psicopedagogo: la solicitud coincide con su fecha de alta.
            migrationBuilder.Sql("EXEC(N'UPDATE Usuario SET FechaSolicitud = FechaAlta')");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "SesionEjercicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Sesion",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Finalizada"); // las sesiones anteriores ya se realizaron

            // Orden de los ejercicios ya cargados: por id de ejercicio dentro de cada sesión.
            migrationBuilder.Sql(@"EXEC(N'
                UPDATE se SET Orden = x.rn
                FROM SesionEjercicio se
                JOIN (SELECT SesionId, EjercicioId, ROW_NUMBER() OVER (PARTITION BY SesionId ORDER BY EjercicioId) AS rn FROM SesionEjercicio) x
                  ON x.SesionId = se.SesionId AND x.EjercicioId = se.EjercicioId')");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Psicopedagogo",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Genero",
                table: "Persona",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "Persona",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Resumen",
                table: "Informe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_Administrador_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialEstadoCuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AdministradorId = table.Column<int>(type: "int", nullable: false),
                    EstadoAnterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstadoNuevo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialEstadoCuenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialEstadoCuenta_Administrador_AdministradorId",
                        column: x => x.AdministradorId,
                        principalTable: "Administrador",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialEstadoCuenta_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CorreoElectronico",
                table: "Usuario",
                column: "CorreoElectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Estado",
                table: "Usuario",
                column: "Estado");

            // Matrículas vacías (cargadas antes de existir el campo): se les asigna un valor único para poder crear el índice.
            migrationBuilder.Sql("UPDATE Psicopedagogo SET Matricula = 'SIN-MATRICULA-' + CAST(PersonaId AS nvarchar(20)) WHERE Matricula = ''");

            migrationBuilder.CreateIndex(
                name: "IX_Psicopedagogo_Matricula",
                table: "Psicopedagogo",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorialEstadoCuenta_AdministradorId",
                table: "HistorialEstadoCuenta",
                column: "AdministradorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialEstadoCuenta_UsuarioId",
                table: "HistorialEstadoCuenta",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Persona_PersonaId",
                table: "Usuario",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Persona_PersonaId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "HistorialEstadoCuenta");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_CorreoElectronico",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Estado",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Psicopedagogo_Matricula",
                table: "Psicopedagogo");

            migrationBuilder.DropColumn(
                name: "AceptoTerminos",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FechaAceptacionTerminos",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FechaSolicitud",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "SesionEjercicio");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Sesion");

            migrationBuilder.DropColumn(
                name: "Resumen",
                table: "Informe");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Usuario",
                newName: "PsicopedagogoId");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Psicopedagogo",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genero",
                table: "Persona",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "Persona",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Psicopedagogo_PsicopedagogoId",
                table: "Usuario",
                column: "PsicopedagogoId",
                principalTable: "Psicopedagogo",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
