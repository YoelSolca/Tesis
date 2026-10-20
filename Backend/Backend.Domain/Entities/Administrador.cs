namespace Backend.Domain.Entities
{
    public class Administrador
    {
        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public ICollection<HistorialEstadoCuenta> CambiosRealizados { get; set; } = new List<HistorialEstadoCuenta>();
    }
}
