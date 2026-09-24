namespace Backend.Domain.Entities
{
    public class Usuario
    {
        public int PsicopedagogoId { get; set; }
        public Psicopedagogo Psicopedagogo { get; set; } = null!;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public string? TokenRestablecimiento { get; set; }
        public DateTime FechaExpiracionTokenRestablecimiento { get; set; }
        public DateTime FechaRecuperacion { get; set; }
        //public DateTime FechaCreacion { get; set; }

    }
}
