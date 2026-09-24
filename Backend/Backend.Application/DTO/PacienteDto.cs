using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace Backend.Application.DTO;

public record PacienteDto(int personaId, string Direccion)
{
    public static PacienteDto FromEntity(Paciente p) => new(p.PersonaId, p.Direccion);
}


public record CreatePacienteRequest(
    [Required] int PsicopedagogoId,
    [Required, StringLength(50)] string Direccion,
    [Required, StringLength(100)] string Nombre,
    [StringLength(100)] string Apellido,
    [StringLength(20)] string Telefono,
    [StringLength(20)] string Documento,
    [StringLength(1)] string Genero,
    DateOnly FechaNacimiento
);

public record UpsertPacienteRequest(
    [Required, StringLength(50)] string Direccion
    );


