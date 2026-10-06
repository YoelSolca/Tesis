namespace Backend.Domain.Common
{
    /// <summary>Una página de resultados y el total de filas que cumplen el filtro.</summary>
    public record PagedResult<T>(IReadOnlyList<T> Items, int Total);
}
