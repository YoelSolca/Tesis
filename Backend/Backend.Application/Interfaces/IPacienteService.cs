using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IPacienteService
    {
        /// <summary>Registra al paciente o, si el documento ya existe, solo vincula al psicopedagogo (sin duplicar).</summary>
        Task<Result<RegistroPacienteResult>> CreatePacienteAsync(CreatePacienteRequest request, CancellationToken ct = default);

        Task<Result<PacienteDetalleDto>> GetDetalleAsync(int pacienteId, CancellationToken ct = default);

        /// <summary>Lista paginada de los pacientes vigentes del psicopedagogo autenticado.</summary>
        Task<Result<PagedResponse<PacienteListItemDto>>> GetPacientesAsync(string? search, int? tipoDificultadId, int page, int pageSize, CancellationToken ct = default);

        Task<Result<PacienteDto>> UpdatePacienteAsync(int pacienteId, UpsertPacienteRequest request, CancellationToken ct = default);

        Task<Result<IntervencionDto>> GetIntervencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);

        Task<Result<IntervencionDto>> UpdateIntervencionAsync(int pacienteId, int psicopedagogoId, UpsertIntervencionRequest request, CancellationToken ct = default);
    }
}
