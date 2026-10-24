using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Queries
{
    public class TipoEjercicioQueries(AppDbContext context) : ITipoEjercicioQueries
    {
        public async Task<IReadOnlyList<TipoEjercicioDto>> GetAllAsync(CancellationToken ct = default)
        => await context.TipoEjercicio.AsNoTracking()
                 .OrderBy(t => t.Nombre)
                 .Select(t => new TipoEjercicioDto(t.Id, t.Nombre))
                 .ToListAsync(ct);
    }
}
