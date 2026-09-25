using Backend.Domain.Entities;

namespace Backend.Application.DTO;

public record EjercicioDto(int id, string nombre, string tipoEjercicio)
{
    public static EjercicioDto FromEntity(Ejercicio e) => new(e.Id, e.Nombre, e.TipoEjercicio.Nombre);
}
