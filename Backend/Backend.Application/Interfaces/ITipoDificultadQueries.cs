using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    /// <summary>Catálogo de tipos de dificultad (solo lectura), para el filtro de la lista y el alta de pacientes.</summary>
    public interface ITipoDificultadQueries
    {
        Task<IReadOnlyList<TipoDificultadDto>> GetAllAsync(CancellationToken ct = default);
    }
}
