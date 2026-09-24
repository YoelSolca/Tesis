using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPersonaRepository
    {
        Task<IReadOnlyList<Persona?>> GetAllAsync(CancellationToken ct = default);

        Task<Persona?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<bool> IdCardExistAsync(string idCard, int? excludedId = null, CancellationToken ct = default);

        Task<Persona?> AddAsync(Persona persona, CancellationToken ct = default);
        Task<Persona?> GetForUpdateAsync(int id, CancellationToken ct = default);

        Task UpdateAsync(Persona persona, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
