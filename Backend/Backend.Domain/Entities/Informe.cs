namespace Backend.Domain.Entities
{
    /// <summary>Informe generado por IA que resume las sesiones de un paciente en un período.</summary>
    public class Informe
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int PsicopedagogoId { get; set; }
        public Psicopedagogo Psicopedagogo { get; set; } = null!;

        public DateTime FechaGeneracion { get; set; }
        public DateTime PeriodoDesde { get; set; }
        public DateTime PeriodoHasta { get; set; }
        public string Contenido { get; set; } = string.Empty;
    }
}
