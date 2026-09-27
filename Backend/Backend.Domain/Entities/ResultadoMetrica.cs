namespace Backend.Domain.Entities
{
    /// <summary>Valor de una métrica específica para un resultado puntual (ej. Omisiones = 3).</summary>
    public class ResultadoMetrica
    {
        public int SesionId { get; set; }
        public int EjercicioId { get; set; }
        public Resultado Resultado { get; set; } = null!;

        public int MetricaId { get; set; }
        public Metrica Metrica { get; set; } = null!;

        public decimal Valor { get; set; }
    }
}
