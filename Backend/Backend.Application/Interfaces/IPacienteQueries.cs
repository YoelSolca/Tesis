using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    /// <summary>Consultas de solo lectura que devuelven DTOs ya proyectados (sin cargar entidades).</summary>
    public interface IPacienteQueries
    {
        /// <summary>Pacientes con atención vigente del psicopedagogo, filtrados por nombre/apellido/documento y paginados.</summary>
        Task<PagedResponse<PacienteListItemDto>> GetPacientesAsync(int psicopedagogoId, string? search, int? tipoDificultadId, int page, int pageSize, CancellationToken ct = default);

        /// <summary>Detalle visto por ese psicopedagogo, o null si no existe o nunca lo atendió.</summary>
        Task<PacienteDetalleDto?> GetDetalleAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);
    }
}
