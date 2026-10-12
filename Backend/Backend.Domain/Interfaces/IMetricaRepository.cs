
namespace Backend.Domain.Interfaces
{
    public interface IMetricaRepository
    {
        Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
    }
}
