using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IPsicopedagogoService
    {
        Task<Result<PsicopedagogoDto>> CreatesicopedagogoAsync(CreatePsicopedagogoRequest request, CancellationToken ct = default);

        Task<Result<PsicopedagogoDto>> GetByPsicopedagogoIdAsync(int psicopedagogoId, CancellationToken ct = default);

        Task<Result<PsicopedagogoDto>> UpdateAsync(int psicopedagogoId, UpsertPsicopedagogoRequest request, CancellationToken ct = default);
    }
}
