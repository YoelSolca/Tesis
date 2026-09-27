namespace Backend.Domain.Entities
{
    /// <summary>Cómo se resumen varios valores de una misma métrica a lo largo del tiempo.</summary>
    public enum TipoAgregacion
    {
        Suma,
        Promedio,
        Maximo
    }
}
