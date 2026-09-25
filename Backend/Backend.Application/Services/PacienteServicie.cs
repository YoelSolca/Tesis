using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class PacienteService(
        IPacienteRepository repository,
        IPersonaRepository personaRepository,
        IPsicopedagogoRepository psicopedagogoRepository,
        ILogger<PacienteService> logger
        ) : IPacienteService
    {
        public async Task<IReadOnlyList<PacienteDto>> GetAllPacienteAsync(CancellationToken ct = default)
        {
           var pacientes = await repository.GetAllPacienteAsync(ct);
            return pacientes.Select(PacienteDto.FromEntity).ToList();
        }

        public async Task<Result<PacienteDto>> GetByPacienteIdAsync(int pacienteId, CancellationToken ct = default)
        {
            var paciente = await repository.GetByPacienteIdAsync(pacienteId, ct);
            return paciente is null
                ? Result<PacienteDto>.Failure($"El paciente con ID {pacienteId} no fue encontrado.", ErrorCodes.NotFound)
                : Result<PacienteDto>.Success(PacienteDto.FromEntity(paciente));
        }

        public async Task<Result<PacienteDto>> CreatePacienteAsync(CreatePacienteRequest request, CancellationToken ct = default)
        {
            if (await personaRepository.IdCardExistAsync(request.Documento, ct: ct))
            {
                return Result<PacienteDto>.Failure($"El paciente con documento {request.Documento} ya existe.", ErrorCodes.Duplicate);
            }

            if(await psicopedagogoRepository.GetByPsicopedagogoIdAsync(request.PsicopedagogoId, ct) is null)
            {
                return Result<PacienteDto>.Failure($"El psicopedagogo con ID {request.PsicopedagogoId} no fue encontrado.", ErrorCodes.NotFound);
            }

            var paciente = new Paciente
            {
                Direccion = request.Direccion,
                FechaAlta = DateTime.Now,
                Persona = new Persona
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Telefono = request.Telefono,
                    Documento = request.Documento,
                    Genero = request.Genero,
                    FechaNacimiento = request.FechaNacimiento
                },
                PsicopedagogoId = request.PsicopedagogoId
            };


            await repository.AddPacienteAsync(paciente, ct);
            logger.LogInformation("Paciente creado: {Id} ({documento})", paciente.PersonaId, paciente.Persona.Documento);
            return Result<PacienteDto>.Success(PacienteDto.FromEntity(paciente));
        }

        public async Task<Result<PacienteDto>> UpdatePacienteAsync(int pacienteId, UpsertPacienteRequest request, CancellationToken ct = default)
        {
            var existing = await repository.GetByPacienteIdAsync(pacienteId, ct);

            if (existing is null)
            {
                return Result<PacienteDto>.Failure($"El paciente con ID {pacienteId} no fue encontrado.", ErrorCodes.NotFound);
            }

            var paciente = new Paciente
            {
                PersonaId = pacienteId,
                Direccion = request.Direccion,
                //Persona = new Persona
                //{
                //    Nombre = request. Nombre,
                //    Apellido = request.Apellido,
                //    Telefono = request.Telefono,
                //    Documento = request.Documento,
                //    Genero = request.Genero,
                //    FechaNacimiento = request.FechaNacimiento
                //}
            };

            await repository.UpdateAsync(paciente, ct);
            logger.LogInformation("Paciente actualizado: {Id} ({documento})", paciente.PersonaId, paciente.Persona.Documento);
            return Result<PacienteDto>.Success(PacienteDto.FromEntity(paciente));

        }
    }
}
