namespace Backend.Domain.Entities
{
    public class Sesion
    {
        public int Id { get; set; }

        // Clave foránea compuesta hacia el vínculo: la sesión es privada del psicopedagogo que la registró.
        public int PsicopedagogoId { get; set; }
        public int PacienteId { get; set; }
        public PsicopedagogoPaciente PsicopedagogoPaciente { get; set; } = null!;

        public DateTime Fecha { get; set; }
    }
}
