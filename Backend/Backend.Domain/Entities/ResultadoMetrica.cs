namespace Backend.Domain.Entities
{
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
