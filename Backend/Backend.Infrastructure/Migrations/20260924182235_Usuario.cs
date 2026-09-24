using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contrasenia",
                table: "Psicopedagogo");

            migrationBuilder.DropColumn(
                name: "CorreoElectronico",
                table: "Psicopedagogo");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Psicopedagogo");

            migrationBuilder.DropColumn(
                name: "FechaExpiracionTokenRestablecimiento",
                table: "Psicopedagogo");

            migrationBuilder.DropColumn(
                name: "FechaRecuperacion",
                table: "Psicopedagogo");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    PsicopedagogoId = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaExpiracionTokenRestablecimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRecuperacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.PsicopedagogoId);
                    table.ForeignKey(
                        name: "FK_Usuario_Psicopedagogo_PsicopedagogoId",
                        column: x => x.PsicopedagogoId,
                        principalTable: "Psicopedagogo",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Contrasenia",
                table: "Psicopedagogo",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorreoElectronico",
                table: "Psicopedagogo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Psicopedagogo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaExpiracionTokenRestablecimiento",
                table: "Psicopedagogo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRecuperacion",
                table: "Psicopedagogo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
