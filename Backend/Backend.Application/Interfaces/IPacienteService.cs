using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<Result<PacienteDto>> CreatePacienteAsync(CreatePacienteRequest request, CancellationToken ct = default);

        Task<Result<PacienteDto>> GetByPacienteIdAsync(int pacienteId, CancellationToken ct = default);
        Task<IReadOnlyList<PacienteDto>> GetAllPacienteAsync(CancellationToken ct = default);

        Task<Result<PacienteDto>> UpdatePacienteAsync(int pacienteId, UpsertPacienteRequest request, CancellationToken ct = default);
    }
}
