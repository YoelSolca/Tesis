namespace Backend.Domain.Entities
{
    /// <summary>
    /// Catálogo de métricas específicas de un ejercicio (ej. "omisiones", "nivelAlcanzado").
    /// Permite agregar ejercicios nuevos con métricas nuevas sin migrar Resultado.
    /// </summary>
    public class Metrica
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string? Unidad { get; set; }

        public TipoAgregacion TipoAgregacion { get; set; }

        public ICollection<ResultadoMetrica> ResultadoMetricas { get; set; } = new List<ResultadoMetrica>();
    }
}
