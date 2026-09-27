namespace Backend.Domain.Entities
{
    /// <summary>
    /// Resultado de un ejercicio hecho dentro de una sesión. 1 a 1 con SesionEjercicio: comparte su misma
    /// clave compuesta. Si el ejercicio se repite, el frontend manda el último resultado y se pisa el anterior.
    /// </summary>
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
