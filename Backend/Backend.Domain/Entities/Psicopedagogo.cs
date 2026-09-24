namespace Backend.Domain.Entities
{
    public class Psicopedagogo
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public DateTime FechaExpiracionTokenRestablecimiento { get; set; }
        public DateTime FechaRecuperacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
