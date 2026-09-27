using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IResultadoRepository
    {
        /// <summary>True si ese SesionEjercicio existe y pertenece a una sesión del psicopedagogo indicado.</summary>
        Task<bool> SesionEjercicioPerteneceAsync(int sesionId, int ejercicioId, int psicopedagogoId, CancellationToken ct = default);

        Task<Resultado?> GetAsync(int sesionId, int ejercicioId, CancellationToken ct = default);

        /// <summary>Crea el resultado si no existía, o lo reemplaza si el ejercicio se repitió.</summary>
        Task<Resultado> UpsertAsync(Resultado resultado, CancellationToken ct = default);
    }
}
