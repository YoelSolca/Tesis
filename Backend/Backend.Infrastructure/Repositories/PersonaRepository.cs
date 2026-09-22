using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class PersonaRepository(AppDbContext context) : IPersonaRepository
    {
        public async Task<IReadOnlyList<Persona?>> GetAllAsync(CancellationToken ct = default)
        {
            var query = context.Persona.AsNoTracking();

            var personas = await query
                          .OrderBy(p => p.Nombre)
                          .ThenBy(p => p.Id)
                          .ToListAsync(ct);

            return personas;
        }

        public async Task<Persona?> GetByIdAsync(int id, CancellationToken ct = default) 
            => await context.Persona.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<Persona?> GetForUpdateAsync(int id, CancellationToken ct = default)
        => await context.Persona.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<bool> IdCardExistAsync(string idCard, int? excludedId = null, CancellationToken ct = default)
        => await context.Persona.AsNoTracking().AnyAsync(p => p.Documento == idCard && (excludedId == null || p.Id != excludedId), ct);

        public async Task<Persona?> AddAsync(Persona persona, CancellationToken ct = default)
        {
            context.Persona.Add(persona);
            await context.SaveChangesAsync(ct);
            return persona;
        }

        public async Task UpdateAsync(Persona persona, CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await context.Persona.Where(p => p.Id == id).ExecuteDeleteAsync(ct) > 0;
    }
}
