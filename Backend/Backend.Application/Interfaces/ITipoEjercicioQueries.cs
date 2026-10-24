using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface ITipoEjercicioQueries
    {
        Task<IReadOnlyList<TipoEjercicioDto>> GetAllAsync(CancellationToken ct = default);
    }
}
