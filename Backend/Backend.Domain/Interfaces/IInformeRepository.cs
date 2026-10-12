using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IInformeRepository
    {
        Task<bool> PacientePerteneceAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);
        Task<IReadOnlyList<Informe>> GetAllAsync(int psicopedagogoId, CancellationToken ct = default);
        Task<Informe?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Informe> AddAsync(Informe informe, CancellationToken ct = default);
    }
}
