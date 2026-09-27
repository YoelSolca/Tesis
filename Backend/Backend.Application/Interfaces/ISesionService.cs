using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface ISesionService
    {
        Task<IReadOnlyList<SesionDto>> GetAllAsync(int psicopedagogoId, int? pacienteId = null, CancellationToken ct = default);

        Task<Result<SesionDto>> GetByIdAsync(int id, int psicopedagogoId, CancellationToken ct = default);

        Task<Result<SesionDto>> AddAsync(CreateSesionRequest sesion, CancellationToken ct = default);

        Task<Result<SesionDto>> UpdateAsync(int id, int psicopedagogoId, UpsertSesionRequest sesion, CancellationToken ct = default);
    }
}
