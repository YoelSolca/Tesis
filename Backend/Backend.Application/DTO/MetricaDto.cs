using System.ComponentModel.DataAnnotations;
using Backend.Domain.Entities;

namespace Backend.Application.DTO;

public record MetricaDto(int Id, string Nombre, string? Descripcion, string? Unidad, string TipoAgregacion)
{
    public static MetricaDto FromEntity(Metrica m) => new(m.Id, m.Nombre, m.Descripcion, m.Unidad, m.TipoAgregacion.ToString());
}

public record CreateMetricaRequest(
    [Required, StringLength(50)] string Nombre,
    [StringLength(200)] string? Descripcion,
    [StringLength(30)] string? Unidad,
    [Required] TipoAgregacion TipoAgregacion
);
