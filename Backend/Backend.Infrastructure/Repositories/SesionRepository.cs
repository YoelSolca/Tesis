using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class SesionRepository(AppDbContext context) : ISesionRepository
    {
        public async Task<IReadOnlyList<Sesion?>> GetAllAsync(int psicopedagogoId, int? pacienteId = null, CancellationToken ct = default)
        {
            var query = context.Sesion.AsNoTracking().Where(s => s.PsicopedagogoId == psicopedagogoId);

            if (pacienteId.HasValue)
                query = query.Where(s => s.PacienteId == pacienteId);

            var sesiones = await query
                          .OrderBy(s => s.Id)
                          .ThenBy(s => s.Id)
                          .ToListAsync(ct);

            return sesiones;
        }

        public async Task<Sesion?> GetByIdAsync(int id, int psicopedagogoId, CancellationToken ct = default)
        => await context.Sesion.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && s.PsicopedagogoId == psicopedagogoId, ct);

        public async Task<Sesion?> AddAsync(Sesion sesion, CancellationToken ct = default)
        {
            context.Sesion.Add(sesion);
            await context.SaveChangesAsync(ct);
            return sesion;
        }

        public async Task UpdateAsync(Sesion sesion, CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
    }
}
