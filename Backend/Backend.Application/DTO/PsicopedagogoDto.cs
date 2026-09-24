using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DTO;

public record PsicopedagogoDto(int PersonaId, string CorreoElectronico, string Contrasenia, string? Avatar)
{
    public static PsicopedagogoDto FromEntity(Psicopedagogo p) => new(p.PersonaId, p.Usuario.CorreoElectronico,p.Usuario.Contrasenia, p.Avatar);
}


public record CreatePsicopedagogoRequest(
    [Required, StringLength(50)] string CorreoElectronico,
    [Required, StringLength(16)] string Contrasenia,
    [StringLength(250)] string? Avatar,
    [Required, StringLength(100)] string Nombre,
    [StringLength(100)] string Apellido,
    [StringLength(20)] string Telefono,
    [StringLength(20)] string Documento,
    [StringLength(1)] string Genero,
    DateOnly FechaNacimiento
);

public record UpsertPsicopedagogoRequest(
    [Required, StringLength(50)] string CorreoElectronico,
    [Required, StringLength(16)] string Contrasenia,
    [StringLength(250)]string? Avatar
);