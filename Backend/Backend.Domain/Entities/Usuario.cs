namespace Backend.Domain.Entities
{
    public class Usuario
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public DateTime FechaAlta{ get; set; }
        public string? TokenRestablecimiento { get; set; }
        public DateTime FechaExpiracionTokenRestablecimiento { get; set; }
        public DateTime FechaRecuperacion { get; set; }
        public string? SecretoDobleFactor { get; set; }
        public bool DobleFactorHabilitado { get; set; }
        public RolUsuario Rol { get; set; }
        public EstadoCuenta Estado { get; set; } = EstadoCuenta.Pendiente;
        public DateTime FechaSolicitud { get; set; }
        public bool AceptoTerminos { get; set; }
        public DateTime? FechaAceptacionTerminos { get; set; }
        public ICollection<HistorialEstadoCuenta> Historial { get; set; } = new List<HistorialEstadoCuenta>();
    }
}
