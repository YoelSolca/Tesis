using System.ComponentModel.DataAnnotations;
using Backend.Domain.Entities;

namespace Backend.Application.DTO;

public record InformeDto(
    int Id,
    int PacienteId,
    int PsicopedagogoId,
    DateTime FechaGeneracion,
    DateTime PeriodoDesde,
    DateTime PeriodoHasta,
    string Contenido)
{
    public static InformeDto FromEntity(Informe i) => new(
        i.Id, i.PacienteId, i.PsicopedagogoId, i.FechaGeneracion, i.PeriodoDesde, i.PeriodoHasta, i.Contenido);
}

/// <summary>Pide generar un informe de IA para el paciente, resumiendo el período indicado.</summary>
public record CreateInformeRequest(
    [Required] int PsicopedagogoId,
    [Required] DateTime PeriodoDesde,
    [Required] DateTime PeriodoHasta
);
