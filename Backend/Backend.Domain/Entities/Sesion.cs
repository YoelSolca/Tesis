namespace Backend.Domain.Entities
{
    public class Sesion
    {
        public int Id { get; set; }
        public int PsicopedagogoId { get; set; }
        public int PacienteId { get; set; }
        public PsicopedagogoPaciente PsicopedagogoPaciente { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public EstadoSesion Estado { get; set; } = EstadoSesion.Planificada;
        public ICollection<SesionEjercicio> SesionEjercicios { get; set; } = new List<SesionEjercicio>();
    }
}
