using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IEjercicioRepository
    {
        Task<IReadOnlyList<Ejercicio>> GetAllEjercicioAsync(CancellationToken ct = default);
    }
}
