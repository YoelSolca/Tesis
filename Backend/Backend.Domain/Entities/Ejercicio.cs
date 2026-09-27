namespace Backend.Domain.Entities
{
    public class Ejercicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int TipoEjercicioId { get; set; }

        public TipoEjercicio TipoEjercicio { get; set; } = null!;
        public ICollection<SesionEjercicio> SesionEjercicios { get; set; } = new List<SesionEjercicio>();

    }
}
