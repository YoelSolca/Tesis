namespace Backend.Domain.Entities
{
    public class Resultado
    {
        public int SesionId { get; set; }
        public int EjercicioId { get; set; }
        public SesionEjercicio SesionEjercicio { get; set; } = null!;
        public int Aciertos { get; set; }
        public int Errores { get; set; }
        public int? TiempoSegundos { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public ICollection<ResultadoMetrica> ResultadoMetricas { get; set; } = new List<ResultadoMetrica>();
    }
}
