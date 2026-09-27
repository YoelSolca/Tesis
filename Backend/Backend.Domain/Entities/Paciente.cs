namespace Backend.Domain.Entities
{
    /// <summary>
    /// Registro único de la persona atendida (identificada por su documento). No contiene historial clínico:
    /// la intervención y las sesiones pertenecen a cada vínculo psicopedagogo-paciente.
    /// </summary>
    public class Paciente
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }

        public ICollection<PsicopedagogoPaciente> PsicopedagogoPacientes { get; set; } = new List<PsicopedagogoPaciente>();
    }
}
