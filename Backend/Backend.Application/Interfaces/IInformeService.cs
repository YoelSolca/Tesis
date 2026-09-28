using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IInformeService
    {
        Task<Result<IReadOnlyList<InformeDto>>> GetAllAsync(int psicopedagogoId, CancellationToken ct = default);

        Task<Result<InformeDto>> GetByIdAsync(int id, CancellationToken ct = default);

        Task<Result<InformeDto>> CreateAsync(int pacienteId, CreateInformeRequest request, CancellationToken ct = default);
    }
}
