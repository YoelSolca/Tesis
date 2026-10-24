using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO;

public record SesionDto(int Id, int PacienteId, int PsicopedagogoId, DateTime Fecha, IReadOnlyList<int> EjercicioIds)
{
    public static SesionDto FromEntity(Sesion s) => new(
        s.Id, s.PacienteId, s.PsicopedagogoId, s.Fecha, s.SesionEjercicios.Select(se => se.EjercicioId).ToList());
}

public record CreateSesionRequest(
    [Required] int PacienteId,
    [Required] int PsicopedagogoId,
    [Required, MinLength(1)] IReadOnlyList<int> EjercicioIds
);

public record UpsertSesionRequest(
  [Required, MinLength(1)] IReadOnlyList<int> EjercicioIds
);
