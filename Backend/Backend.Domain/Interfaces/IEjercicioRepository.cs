using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IEjercicioRepository
    {
        Task<IReadOnlyList<Ejercicio>> GetAllEjercicioAsync(CancellationToken ct = default);
        Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
    }
}
