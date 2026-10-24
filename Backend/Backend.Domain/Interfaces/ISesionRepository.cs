using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface ISesionRepository
    {
        Task<IReadOnlyList<Sesion?>> GetAllAsync(int psicopedagogoId, int? pacienteId = null, CancellationToken ct = default);
        Task<Sesion?> GetByIdAsync(int id, int psicopedagogoId, CancellationToken ct = default);
        Task<Sesion?> AddAsync(Sesion sesion, CancellationToken ct = default);
        Task<Sesion?> AgregarEjerciciosAsync(int sesionId, int psicopedagogoId, IEnumerable<int> ejercicioIds, CancellationToken ct = default);
    }
}
