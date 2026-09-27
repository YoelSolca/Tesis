using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class MetricaRepository(AppDbContext context) : IMetricaRepository
    {
        public async Task<IReadOnlyList<Metrica>> GetAllAsync(CancellationToken ct = default)
            => await context.Metrica.AsNoTracking().OrderBy(m => m.Nombre).ToListAsync(ct);

        public Task<bool> NombreExisteAsync(string nombre, CancellationToken ct = default)
            => context.Metrica.AnyAsync(m => m.Nombre == nombre, ct);

        public async Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
            => await context.Metrica.Where(m => ids.Contains(m.Id)).Select(m => m.Id).ToListAsync(ct);

        public async Task<Metrica> AddAsync(Metrica metrica, CancellationToken ct = default)
        {
            context.Metrica.Add(metrica);
            await context.SaveChangesAsync(ct);
            return metrica;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
            => await context.Metrica.Where(m => m.Id == id).ExecuteDeleteAsync(ct) > 0;
    }
}
