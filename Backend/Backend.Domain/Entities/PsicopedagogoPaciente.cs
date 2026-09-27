namespace Backend.Domain.Entities
{
    /// <summary>
    /// Atención de un psicopedagogo a un paciente. Es la relación muchos a muchos y, además, el dueño
    /// del historial privado: su intervención y sus sesiones. Cada psicopedagogo empieza de cero con el paciente.
    /// Finalizar la atención cierra FechaFin; no se borra la fila, porque arrastraría el historial.
    /// </summary>
    public class PsicopedagogoPaciente
    {
        public int PacienteId { get; set; }

        public Paciente Paciente { get; set; } = null!;

        public int PsicopedagogoId { get; set; }

        public Psicopedagogo Psicopedagogo { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        /// <summary>Null mientras la atención está vigente.</summary>
        public DateTime? FechaFin { get; set; }

        public int IntervencionId { get; set; }

        public Intervencion Intervencion { get; set; } = null!;

        public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
    }
}
