using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO;

    public record PersonaDto(
         int Id,
         string Nombre,
         string Apellido,
         string Telefono,
         string Documento,
         string Genero,
         DateOnly FechaNacimiento)
    {
    public static PersonaDto FromEntity(Persona p) => new(
        p.Id,
        p.Nombre,
        p.Apellido,
        p.Telefono,
        p.Documento,
        p.Genero,
        p.FechaNacimiento 
    );
}

    public record CreatePersonaRequest(
        [Required, StringLength(100)] string Nombre,
        [StringLength(100)] string Apellido,
        [StringLength(20)] string Telefono,
        [StringLength(20)] string Documento,
        [StringLength(1)] string Genero,
        DateOnly FechaNacimiento
    );

    public record UpdatePersonaRequest(
        [Required, StringLength(100)] string Nombre,
        [StringLength(100)] string Apellido,
        [StringLength(20)] string Telefono,
        [StringLength(20)] string Documento,
        [StringLength(1)] string Genero,
        DateOnly FechaNacimiento
    );