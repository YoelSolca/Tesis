using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class PacienteRepository(AppDbContext context) : IPacienteRepository
    {
        public async Task<IReadOnlyList<Paciente?>> GetAllPacienteAsync(CancellationToken ct = default)
        {
            var query = context.Paciente.AsNoTracking();

            var pacientes = await 
                            query.OrderBy(p => p.Persona.Nombre)
                            .ThenBy(p => p.PersonaId)
                            .ToListAsync(ct);
            return pacientes;
        }

        public async Task<Paciente?> GetByPacienteIdAsync(int id, CancellationToken ct = default)
        => await context.Paciente.AsNoTracking().FirstOrDefaultAsync(p => p.PersonaId == id, ct);

        public async Task<Paciente> AddPacienteAsync(Paciente paciente, CancellationToken ct = default)
        {
            await context.Paciente.AddAsync(paciente);
            await context.SaveChangesAsync(ct);
            return paciente;
        }

        public async Task UpdateAsync(Paciente paciente, CancellationToken ct = default)
        {
            context.Paciente.Update(paciente);
            await context.SaveChangesAsync(ct);
        }
    }
}
