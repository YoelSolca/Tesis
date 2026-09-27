using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Backend.Application.DTO;

public record SesionDto(int Id, int PacienteId, int PsicopedagogoId, DateTime Fecha, IReadOnlyList<int> EjercicioIds)
{
    public static SesionDto FromEntity(Sesion s) => new(
        s.Id, s.PacienteId, s.PsicopedagogoId, s.Fecha, s.SesionEjercicios.Select(se => se.EjercicioId).ToList());
}


/// <summary>Crea la sesión con uno o varios ejercicios ya cargados.</summary>
public record CreateSesionRequest(
    [Required] int PacienteId,
    [Required] int PsicopedagogoId,
    [Required, MinLength(1)] IReadOnlyList<int> EjercicioIds
);


/// <summary>Suma uno o varios ejercicios a una sesión ya creada (no reemplaza los que ya tenía).</summary>
public record UpsertSesionRequest(
  [Required, MinLength(1)] IReadOnlyList<int> EjercicioIds
);
