using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class PsicopedagogoRepository(AppDbContext context) : IPsicopedagogoRepository
    {
        public async Task<Psicopedagogo> AddPsicopedagogoAsync(Psicopedagogo psicopedagogo, CancellationToken ct = default)
        {
            context.Psicopedagogo.Add(psicopedagogo);
            await context.SaveChangesAsync(ct);
            return psicopedagogo;
        }

        public Task<Psicopedagogo?> GetByPsicopedagogoIdAsync(int psicopedagogoId, CancellationToken ct = default) 
            => context.Psicopedagogo.AsNoTracking().FirstOrDefaultAsync(p => p.PersonaId == psicopedagogoId, ct);

        public async Task UpdateAsync(Psicopedagogo psicopedagogo, CancellationToken ct = default)
        {
            var existingPsicopedagogo = await context.Psicopedagogo.FirstOrDefaultAsync(p => p.PersonaId == psicopedagogo.PersonaId, ct);
            if (existingPsicopedagogo == null) return;

            existingPsicopedagogo.CorreoElectronico = psicopedagogo.CorreoElectronico;
            existingPsicopedagogo.Contrasenia = psicopedagogo.Contrasenia;
            existingPsicopedagogo.Avatar = psicopedagogo.Avatar;

            //existingPsicopedagogo.FechaExpiracionTokenRestablecimiento = psicopedagogo.FechaExpiracionTokenRestablecimiento;
            //existingPsicopedagogo.FechaRecuperacion = psicopedagogo.FechaRecuperacion;
            //existingPsicopedagogo.FechaCreacion = psicopedagogo.FechaCreacion;

            await context.SaveChangesAsync(ct);
        }
    }
}
