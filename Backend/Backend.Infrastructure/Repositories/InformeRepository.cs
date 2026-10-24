using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class InformeRepository(AppDbContext context) : IInformeRepository
    {
        public Task<bool> PacientePerteneceAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default)
            => context.Set<PsicopedagogoPaciente>().AnyAsync(
                pp => pp.PacienteId == pacienteId && pp.PsicopedagogoId == psicopedagogoId, ct);

        public async Task<IReadOnlyList<Informe>> GetAllAsync(int psicopedagogoId, CancellationToken ct = default)
            => await context.Informe.AsNoTracking()
                .Where(i => i.PsicopedagogoId == psicopedagogoId)
                .OrderByDescending(i => i.FechaGeneracion)
                .ToListAsync(ct);

        public Task<Informe?> GetByIdAsync(int id, CancellationToken ct = default)
            => context.Informe.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, ct);

        public async Task<Informe> AddAsync(Informe informe, CancellationToken ct = default)
        {
            context.Informe.Add(informe);
            await context.SaveChangesAsync(ct);
            return informe;
        }
    }
}
