using Backend.Application.Common;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Backend.Application.DTO;

namespace Backend.Application.Services
{
    public class PersonaService(
        IPersonaRepository repository,
        ILogger<PersonaService> logger) : IPersonaService
    {
        async Task<IReadOnlyList<PersonaDto>> IPersonaService.GetAllAsync(CancellationToken ct)
        {
            var personas = await repository.GetAllAsync(ct);

            return personas.Select(PersonaDto.FromEntity).ToList();
        }

        public async Task<Result<PersonaDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var persona = await repository.GetByIdAsync(id, ct);

            return persona is null 
                ? Result<PersonaDto>.Failure($"La perosona con ID {id} no fue encontrada.", ErrorCodes.NotFound)
                : Result<PersonaDto>.Success(PersonaDto.FromEntity(persona));
        }
        public async Task<Result<PersonaDto>> CreateAsync(CreatePersonaRequest request, CancellationToken ct = default)
        {
            if(await repository.IdCardExistAsync(request.Documento, ct: ct))
            {
                return Result<PersonaDto>.Failure($"La persona con documento {request.Documento} ya existe.", ErrorCodes.Duplicate);
            }

            var persona = new Persona
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                Documento = request.Documento,
                Genero = request.Genero,
                FechaNacimiento = request.FechaNacimiento
            };

            await repository.AddAsync(persona, ct);

            logger.LogInformation("Persona creada: {Id} ({documento})", persona.Id, persona.Documento);

            return Result<PersonaDto>.Success(PersonaDto.FromEntity(persona));
        }

        public async Task<Result<PersonaDto>> UpdateAsync(int id, UpdatePersonaRequest request, CancellationToken ct = default)
        {
            var persona = await repository.GetForUpdateAsync(id, ct);

            if (persona is null)
                return Result<PersonaDto>.Failure($"La persona con ID {id} no fue encontrada.", ErrorCodes.NotFound);

            if(await repository.IdCardExistAsync(request.Documento, excludedId: id, ct: ct))
                return Result<PersonaDto>.Failure($"La persona con documento {request.Documento} ya existe.", ErrorCodes.Duplicate);

            persona.Nombre = request.Nombre;
            persona.Apellido = request.Apellido;
            persona.Telefono = request.Telefono;
            persona.Documento = request.Documento;
            persona.Genero = request.Genero;
            persona.FechaNacimiento = request.FechaNacimiento;

            await repository.UpdateAsync(persona, ct);

            logger.LogInformation("Persona actualizada: {Id} ({documento})", persona.Id, persona.Documento);
            return Result<PersonaDto>.Success(PersonaDto.FromEntity(persona));
        }

        public async Task<Result<bool>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var persona = await repository.GetByIdAsync(id, ct);

            if (!await repository.DeleteAsync(id, ct))
                return Result<bool>.Failure($"La persona con ID {id} no fue encontrada.", ErrorCodes.NotFound);


            logger.LogInformation("Persona eliminada: {Id} ({documento})", persona.Id, persona.Documento);
            return Result<bool>.Success(true);
        }


    }
}
