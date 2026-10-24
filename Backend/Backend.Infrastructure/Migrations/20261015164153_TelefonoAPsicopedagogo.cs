using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TelefonoAPsicopedagogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Psicopedagogo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            // Conserva el teléfono de los psicopedagogos existentes antes de quitarlo de Persona.
            migrationBuilder.Sql(@"
                UPDATE ps SET ps.Telefono = p.Telefono
                FROM Psicopedagogo ps
                INNER JOIN Persona p ON p.Id = ps.PersonaId");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Persona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Persona",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                UPDATE p SET p.Telefono = ps.Telefono
                FROM Persona p
                INNER JOIN Psicopedagogo ps ON ps.PersonaId = p.Id");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Psicopedagogo");
        }
    }
}
