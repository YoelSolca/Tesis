using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace Backend.Application.DTO;

public record PacienteDto(int personaId, string Direccion, DateTime FechaAlta)
{
    public static PacienteDto FromEntity(Paciente p) => new(p.PersonaId, p.Direccion, p.FechaAlta);
}

/// <summary>Creado = true si la persona se registró por primera vez; false si ya existía y solo se vinculó/reactivó la atención.</summary>
public record RegistroPacienteResult(PacienteDto Paciente, bool Creado);


/// <summary>
/// Registra un paciente o, si el documento ya existe en el sistema, solo vincula al psicopedagogo con el existente.
/// Objetivo y observaciones inician la intervención privada de ESTE psicopedagogo con el paciente.
/// </summary>
public record CreatePacienteRequest(
    [Required] int PsicopedagogoId,
    [Required, StringLength(50)] string Direccion,
    [Required, StringLength(100)] string Nombre,
    [StringLength(100)] string Apellido,
    [StringLength(20)] string Telefono,
    [StringLength(20)] string Documento,
    [StringLength(1)] string Genero,
    DateOnly FechaNacimiento,
    DateTime FechaAlta,
    [StringLength(50)] string? Objetivo,
    [StringLength(100)] string? Observaciones
);

/// <summary>Solo datos personales del paciente. Objetivo y observaciones se editan en la intervención de cada psicopedagogo.</summary>
public record UpsertPacienteRequest(
    [Required, StringLength(50)] string Direccion,
    [Required, StringLength(100)] string Nombre,
    [StringLength(100)] string Apellido,
    [StringLength(20)] string Telefono,
    [StringLength(20)] string Documento,
    [StringLength(1)] string Genero,
    DateOnly FechaNacimiento,
    DateTime FechaAlta
    );
