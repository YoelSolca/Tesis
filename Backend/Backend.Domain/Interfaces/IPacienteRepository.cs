using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente> AddPacienteAsync(Paciente paciente, CancellationToken ct = default);

        Task<Paciente?> GetByDocumentoAsync(string documento, CancellationToken ct = default);

        /// <summary>True si todos los ids existen en el catálogo de tipos de dificultad.</summary>
        Task<bool> TiposDificultadExistenAsync(IReadOnlyCollection<int> ids, CancellationToken ct = default);

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
