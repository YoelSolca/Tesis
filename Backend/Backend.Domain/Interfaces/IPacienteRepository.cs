using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<IReadOnlyList<Paciente?>> GetAllPacienteAsync(CancellationToken ct = default);

        Task<Paciente> AddPacienteAsync(Paciente paciente, CancellationToken ct = default);

        Task<Paciente?> GetByPacienteIdAsync(int id, CancellationToken ct = default);

        Task UpdateAsync(Paciente paciente, CancellationToken ct = default);
    }
}
