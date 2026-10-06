namespace Backend.Domain.ReadModels
{
    /// <summary>Datos del paciente + intervención privada del psicopedagogo, para la pantalla Información.</summary>
    public record PacienteDetalle(
        int Id, string Nombre, string Apellido, string Documento, DateOnly FechaNacimiento,
        string Telefono, string Direccion, string Genero,
        string? Objetivo, string? Observaciones,
        UltimaSesionResumen? UltimaSesion);

    public record UltimaSesionResumen(int Id, DateTime Fecha, IReadOnlyList<string> TiposEjercicio);
}
