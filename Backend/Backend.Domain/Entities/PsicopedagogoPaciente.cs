namespace Backend.Domain.Entities
{
    public class PsicopedagogoPaciente
    {
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        public int PsicopedagogoId { get; set; }
        public Psicopedagogo Psicopedagogo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
        public int IntervencionId { get; set; }
        public Intervencion Intervencion { get; set; } = null!;
        public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
    }
}
