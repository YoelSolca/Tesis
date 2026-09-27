using System.ComponentModel.DataAnnotations;
using Backend.Domain.Entities;

namespace Backend.Application.DTO;

public record ResultadoMetricaDto(int MetricaId, string Nombre, decimal Valor, string? Unidad)
{
    public static ResultadoMetricaDto FromEntity(ResultadoMetrica rm) =>
        new(rm.MetricaId, rm.Metrica.Nombre, rm.Valor, rm.Metrica.Unidad);
}

public record ResultadoDto(
    int SesionId,
    int EjercicioId,
    int Aciertos,
    int Errores,
    int? TiempoSegundos,
    string? Observaciones,
    DateTime FechaRegistro,
    IReadOnlyList<ResultadoMetricaDto> Metricas)
{
    public static ResultadoDto FromEntity(Resultado r) => new(
        r.SesionId, r.EjercicioId, r.Aciertos, r.Errores, r.TiempoSegundos, r.Observaciones, r.FechaRegistro,
        r.ResultadoMetricas.Select(ResultadoMetricaDto.FromEntity).ToList());
}

public record MetricaValorRequest(
    [Required] int MetricaId,
    [Required] decimal Valor
);

/// <summary>Crea el resultado del ejercicio, o lo reemplaza si se repitió dentro de la misma sesión.</summary>
public record UpsertResultadoRequest(
    [Required] int PsicopedagogoId,
    [Range(0, int.MaxValue)] int Aciertos,
    [Range(0, int.MaxValue)] int Errores,
    [Range(0, int.MaxValue)] int? TiempoSegundos,
    [StringLength(500)] string? Observaciones,
    IReadOnlyList<MetricaValorRequest>? Metricas
);
