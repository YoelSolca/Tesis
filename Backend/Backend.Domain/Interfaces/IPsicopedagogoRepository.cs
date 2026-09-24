using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPsicopedagogoRepository
    {
        Task<Psicopedagogo> AddPsicopedagogoAsync(Psicopedagogo psicopedagogo, CancellationToken ct = default);

        Task<Psicopedagogo?> GetByPsicopedagogoIdAsync(int id, CancellationToken ct = default);

        Task UpdateAsync(Psicopedagogo psicopedagogo, CancellationToken ct = default);
    }
}
