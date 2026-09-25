namespace Backend.Domain.Entities
{
    public class Paciente
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public string Direccion { get; set; } = string.Empty;
        public int PsicopedagogoId { get; set; }
        public Psicopedagogo Psicopedagogo { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
    }
}
