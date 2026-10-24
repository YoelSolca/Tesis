using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Queries
{
    public class TipoDificultadQueries(AppDbContext context) : ITipoDificultadQueries
    {
        public async Task<IReadOnlyList<TipoDificultadDto>> GetAllAsync(CancellationToken ct = default)
            => await context.TipoDificultad.AsNoTracking()
                .OrderBy(t => t.Nombre)
                .Select(t => new TipoDificultadDto(t.Id, t.Nombre))
                .ToListAsync(ct);
    }
}
