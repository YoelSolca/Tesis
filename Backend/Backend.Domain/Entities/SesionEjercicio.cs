namespace Backend.Domain.Entities
{
    public class SesionEjercicio
    {


        public int SesionId { get; set; }   

        public Sesion Sesion { get; set; } = null!;

        public int EjercicioId { get; set; }

        public Ejercicio Ejercicio { get; set; } = null!;

        public Resultado? Resultado { get; set; }

    }
}
