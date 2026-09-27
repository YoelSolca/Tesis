using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class EjercicioRepository(AppDbContext context) : IEjercicioRepository
    {
        public async Task<IReadOnlyList<Ejercicio?>> GetAllEjercicioAsync(CancellationToken ct = default)
        {
            var query = context.Ejercicio.AsNoTracking();

            var ejercicios = await query
                           .OrderBy(e => e.Nombre)
                           .ThenBy(e => e.Id)
                           .Include(e => e.TipoEjercicio)
                           .ToListAsync(ct);

            return ejercicios;
        }

        public async Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
            => await context.Ejercicio.Where(e => ids.Contains(e.Id)).Select(e => e.Id).ToListAsync(ct);
    }
}
