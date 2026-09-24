namespace Backend.Domain.Entities
{
    public class Psicopedagogo
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public string? Avatar { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
