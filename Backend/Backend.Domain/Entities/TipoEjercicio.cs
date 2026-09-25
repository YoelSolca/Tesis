namespace Backend.Domain.Entities
{
    public class TipoEjercicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string icono { get; set; } = string.Empty;

        public ICollection<Ejercicio> Ejercicios { get; set; } = new List<Ejercicio>();

    }
}
