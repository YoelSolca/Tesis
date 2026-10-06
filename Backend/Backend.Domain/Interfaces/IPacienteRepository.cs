using Backend.Domain.Entities;
using Backend.Domain.Common;
using Backend.Domain.ReadModels;

namespace Backend.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        /// <summary>Pacientes con atención vigente (sin FechaFin) del psicopedagogo, filtrados por nombre/apellido/documento y paginados.</summary>
        Task<PagedResult<PacienteResumen>> GetPacientesAsync(int idPsicopedagogo, string? search, int page, int pageSize, CancellationToken ct = default);

        Task<Paciente> AddPacienteAsync(Paciente paciente, CancellationToken ct = default);

        Task<Paciente?> GetByPacienteIdAsync(int id, CancellationToken ct = default);

        Task<Paciente?> GetByDocumentoAsync(string documento, CancellationToken ct = default);

        /// <summary>Lookup con seguimiento y la Persona cargada, para modificar y guardar con <see cref="UpdateAsync"/>.</summary>
        Task<Paciente?> GetForUpdateAsync(int id, CancellationToken ct = default);

        Task UpdateAsync(Paciente paciente, CancellationToken ct = default);

        /// <summary>Vínculo psicopedagogo-paciente (vigente o finalizado), o null si nunca lo atendió.</summary>
        Task<PsicopedagogoPaciente?> GetAtencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);

        Task IniciarAtencionAsync(PsicopedagogoPaciente atencion, CancellationToken ct = default);

        /// <summary>Reabre una atención finalizada conservando su historial. False si no había ninguna finalizada.</summary>
        Task<bool> ReactivarAtencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);

        /// <summary>Cierra la atención vigente sin borrar su historial. False si no había atención vigente.</summary>
        Task<bool> FinalizarAtencionAsync(int pacienteId, int psicopedagogoId, DateTime fechaFin, CancellationToken ct = default);

        Task<Intervencion?> GetIntervencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default);

        /// <summary>False si el psicopedagogo nunca atendió al paciente.</summary>
        Task<bool> UpdateIntervencionAsync(int pacienteId, int psicopedagogoId, string? objetivo, string? observaciones, CancellationToken ct = default);
    }
}
