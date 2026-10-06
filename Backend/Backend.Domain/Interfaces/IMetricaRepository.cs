using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IMetricaRepository
    {
        /// <summary>Ids de métricas que sí existen, para validar antes de asignarlas a un resultado.</summary>
        Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
    }
}
