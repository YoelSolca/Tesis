using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IMetricaRepository
    {
        Task<IReadOnlyList<Metrica>> GetAllAsync(CancellationToken ct = default);

        Task<bool> NombreExisteAsync(string nombre, CancellationToken ct = default);

        /// <summary>Ids de métricas que sí existen, para validar antes de asignarlas a un resultado.</summary>
        Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);

        Task<Metrica> AddAsync(Metrica metrica, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
