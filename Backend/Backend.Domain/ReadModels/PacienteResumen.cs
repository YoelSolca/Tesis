namespace Backend.Domain.ReadModels
{
    /// <summary>Proyección de solo lectura para listar pacientes (sin cargar la entidad completa).</summary>
    public record PacienteResumen(int Id, string Nombre, string? Apellido, string? Documento, DateOnly FechaNacimiento);
}
