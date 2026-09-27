using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IMetricaService
    {
        Task<IReadOnlyList<MetricaDto>> GetAllAsync(CancellationToken ct = default);
        Task<Result<MetricaDto>> CreateAsync(CreateMetricaRequest request, CancellationToken ct = default);
        Task<Result<bool>> DeleteAsync(int id, CancellationToken ct = default);
    }
}
