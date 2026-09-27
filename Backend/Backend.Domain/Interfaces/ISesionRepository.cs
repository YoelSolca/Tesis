using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface ISesionRepository
    {
        /// <summary>Sesiones registradas por el psicopedagogo, opcionalmente de un solo paciente.</summary>
        Task<IReadOnlyList<Sesion?>> GetAllAsync(int psicopedagogoId, int? pacienteId = null, CancellationToken ct = default);

        /// <summary>Solo devuelve la sesión si pertenece al psicopedagogo indicado.</summary>
        Task<Sesion?> GetByIdAsync(int id, int psicopedagogoId, CancellationToken ct = default);

        Task<Sesion?> AddAsync(Sesion sesion, CancellationToken ct = default);

        /// <summary>
        /// Suma ejercicios (los que aún no estuvieran) a una sesión existente del psicopedagogo indicado.
        /// Null si la sesión no existe o no es suya.
        /// </summary>
        Task<Sesion?> AgregarEjerciciosAsync(int sesionId, int psicopedagogoId, IEnumerable<int> ejercicioIds, CancellationToken ct = default);
    }
}
