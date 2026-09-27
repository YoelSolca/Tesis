using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO;

public record SesionDto(int Id, DateTime Fecha, int PacienteId, int PsicopedagogoId)
{
    public static SesionDto FromEntity(Sesion s) => new(s.Id, s.Fecha, s.PacienteId, s.PsicopedagogoId);
}


public record CreateSesionRequest(
    [Required]int PacienteId,
    [Required]int PsicopedagogoId,
    DateTime Fecha
);


public record UpsertSesionRequest(
);
