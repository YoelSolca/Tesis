using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IEjercicioRepository
    {
        Task<IReadOnlyList<Ejercicio>> GetAllEjercicioAsync(CancellationToken ct = default);

        /// <summary>Ids de ejercicios que sí existen, para validar antes de asignarlos a una sesión.</summary>
        Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
    }
}
