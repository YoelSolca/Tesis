using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Backend.Infrastructure.Repositories
{
    public class EjercicioRepository(AppDbContext context) : IEjercicioRepository
    {
        public async Task<IReadOnlyList<Ejercicio?>> GetAllEjercicioAsync(int tipoEjercicioId, int page, int pageSize, CancellationToken ct = default)
        {
            var query = context.Ejercicio.AsNoTracking();

            var ejercicios = await query
                           .OrderBy(e => e.Nombre)
                           .ThenBy(e => e.Id)
                           .Include(e => e.TipoEjercicio)
                           .ToListAsync(ct);

            return ejercicios;
        }

        public async Task<(IReadOnlyList<Ejercicio> Items, int TotalCount)> GetAllEjercicioAsync(EjercicioSearchFilter filter, int skip, int take, CancellationToken ct = default)
        {
            var query =  context.Ejercicio.AsNoTracking()
                                .Include(e => e.TipoEjercicio)
                                .AsQueryable();


            if (filter.TipoEjercicioId.HasValue)
                query = query.Where(e => e.TipoEjercicioId == filter.TipoEjercicioId);



            var total = await query.CountAsync(ct);

            var items = await query.OrderBy(e => e.Nombre)
                             .ThenBy(e => e.Id)
                             .Skip(skip)
                             .Take(take)
                             .ToListAsync(ct);

            return (items, total);
        }

        public async Task<IReadOnlyList<int>> GetExistingIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
            => await context.Ejercicio.Where(e => ids.Contains(e.Id)).Select(e => e.Id).ToListAsync(ct);
    }
}
