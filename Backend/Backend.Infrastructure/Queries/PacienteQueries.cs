using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Queries
{
    public class PacienteQueries(AppDbContext context) : IPacienteQueries
    {
        public async Task<PagedResponse<PacienteListItemDto>> GetPacientesAsync(int psicopedagogoId, string? search, int? tipoDificultadId, int page, int pageSize, CancellationToken ct = default)
        {
            var query = context.Paciente.AsNoTracking()
                .Where(p => p.PsicopedagogoPacientes.Any(pp => pp.PsicopedagogoId == psicopedagogoId && pp.FechaFin == null));

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(p => p.Persona.Nombre.Contains(term)
                                      || p.Persona.Apellido.Contains(term)
                                      || p.Persona.Documento.Contains(term));
            }

            if (tipoDificultadId is int tipoId)
            {
                query = query.Where(p => p.PsicopedagogoPacientes.Any(pp => pp.PsicopedagogoId == psicopedagogoId
                    && pp.Intervencion.TiposDificultad.Any(t => t.TipoDificultadId == tipoId)));
            }

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(p => p.Persona.Apellido)
                .ThenBy(p => p.Persona.Nombre)
                .ThenBy(p => p.PersonaId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PacienteListItemDto(
                    p.PersonaId, p.Persona.Nombre, p.Persona.Apellido, p.Persona.Documento, p.Persona.FechaNacimiento,
                    p.PsicopedagogoPacientes
                        .Where(pp => pp.PsicopedagogoId == psicopedagogoId)
                        .SelectMany(pp => pp.Intervencion.TiposDificultad)
                        .Select(t => t.TipoDificultad.Nombre)
                        .ToList()))
                .ToListAsync(ct);

            return new PagedResponse<PacienteListItemDto>(items, total, page, pageSize);
        }

        public async Task<PacienteDetalleDto?> GetDetalleAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default)
        {
            var atenciones = context.Set<PsicopedagogoPaciente>().AsNoTracking()
                .Where(x => x.PacienteId == pacienteId && x.PsicopedagogoId == psicopedagogoId);

            // Consulta 1: datos personales + intervención. Al partir del vínculo, si no existe devuelve null.
            var datos = await atenciones.Select(x => new
            {
                x.PacienteId,
                x.Paciente.Persona.Nombre,
                x.Paciente.Persona.Apellido,
                x.Paciente.Persona.Documento,
                x.Paciente.Persona.FechaNacimiento,
                x.Paciente.Persona.Telefono,
                x.Paciente.Persona.Genero,
                x.Paciente.Direccion,
                x.Intervencion.objetivo,
                x.Intervencion.observaciones
            }).FirstOrDefaultAsync(ct);

            if (datos is null) return null;

            // Consulta 2: última sesión con los tipos de ejercicio distintos que se trabajaron.
            var ultima = await atenciones.SelectMany(x => x.Sesiones)
                .OrderByDescending(s => s.Fecha)
                .Select(s => new
                {
                    s.Id,
                    s.Fecha,
                    Tipos = s.SesionEjercicios.Select(se => se.Ejercicio.TipoEjercicio.Nombre).Distinct().ToList()
                })
                .FirstOrDefaultAsync(ct);

            // Consulta 3: dificultades trabajadas en la intervención.
            var dificultades = await atenciones.SelectMany(x => x.Intervencion.TiposDificultad)
                .Select(t => new TipoDificultadDto(t.TipoDificultad.Id, t.TipoDificultad.Nombre))
                .ToListAsync(ct);

            return new PacienteDetalleDto(datos.PacienteId, datos.Nombre, datos.Apellido, datos.Documento,
                datos.FechaNacimiento, datos.Telefono, datos.Direccion, datos.Genero,
                datos.objetivo, datos.observaciones, dificultades,
                ultima is null ? null : new UltimaSesionDto(ultima.Id, ultima.Fecha, ultima.Tipos));
        }
    }
}
