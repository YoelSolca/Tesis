namespace Backend.Domain.Entities
{
    public class Psicopedagogo
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public string? Avatar { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public ICollection<PsicopedagogoPaciente> PsicopedagogoPacientes { get; set; } = new List<PsicopedagogoPaciente>();
    }
}
