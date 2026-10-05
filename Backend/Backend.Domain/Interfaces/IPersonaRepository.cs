using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPersonaRepository
    {
        Task<bool> IdCardExistAsync(string idCard, int? excludedId = null, CancellationToken ct = default);
    }
}
