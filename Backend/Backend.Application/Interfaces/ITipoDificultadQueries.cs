using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface ITipoDificultadQueries
    {
        Task<IReadOnlyList<TipoDificultadDto>> GetAllAsync(CancellationToken ct = default);
    }
}
