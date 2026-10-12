using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente> AddPacienteAsync(Paciente paciente, CancellationToken ct = default);
        Task<Paciente?> GetByDocumentoAsync(string documento, CancellationToken ct = default);
        Task<bool> TiposDificultadExistenAsync(IReadOnlyCollection<int> ids, CancellationToken ct = default);
        Task<Paciente?> GetForUpdateAsync(int id, CancellationToken ct = default);
        Task UpdateAsync(Paciente paciente, CancellationToken ct = default);
        Task<PsicopedagogoPaciente?> GetAtencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);
        Task IniciarAtencionAsync(PsicopedagogoPaciente atencion, CancellationToken ct = default);
        Task<bool> ReactivarAtencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);
        Task<bool> FinalizarAtencionAsync(int pacienteId, int psicopedagogoId, DateTime fechaFin, CancellationToken ct = default);
        Task<Intervencion?> GetIntervencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);
        Task<bool> UpdateIntervencionAsync(int pacienteId, int psicopedagogoId, string? objetivo, string? observaciones, CancellationToken ct = default);
    }
}
