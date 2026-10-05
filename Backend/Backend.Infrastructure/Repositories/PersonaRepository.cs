using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class PersonaRepository(AppDbContext context) : IPersonaRepository
    {
        public async Task<bool> IdCardExistAsync(string idCard, int? excludedId = null, CancellationToken ct = default)
        => await context.Persona.AsNoTracking().AnyAsync(p => p.Documento == idCard && (excludedId == null || p.Id != excludedId), ct);
    }
}
