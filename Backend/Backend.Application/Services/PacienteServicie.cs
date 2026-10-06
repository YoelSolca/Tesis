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
        public async Task<IReadOnlyList<PacienteDto>> GetAllPacienteAsync(int idPsicopedagogo, CancellationToken ct = default)
        {
            var pacientes = await repository.GetAllPacienteAsync(idPsicopedagogo, ct);
           return pacientes.Select(PacienteDto.FromEntity).ToList();
        }

        public async Task<Result<PacienteDto>> GetByPacienteIdAsync(int pacienteId, CancellationToken ct = default)
        {
            var paciente = await repository.GetByPacienteIdAsync(pacienteId, ct);
            return paciente is null
                ? Result<PacienteDto>.Failure($"El paciente con ID {pacienteId} no fue encontrado.", ErrorCodes.NotFound)
                : Result<PacienteDto>.Success(PacienteDto.FromEntity(paciente));
        }

        public async Task<Result<RegistroPacienteResult>> CreatePacienteAsync(CreatePacienteRequest request, CancellationToken ct = default)
        {
            if (await psicopedagogoRepository.GetByPsicopedagogoIdAsync(request.PsicopedagogoId, ct) is null)
            {
                return Result<RegistroPacienteResult>.Failure($"El psicopedagogo con ID {request.PsicopedagogoId} no fue encontrado.", ErrorCodes.NotFound);
            }

            // El paciente es único en todo el sistema: si el documento ya existe no se duplica, solo se
            // vincula a este psicopedagogo. Los datos personales guardados no se sobrescriben.
            var existente = await repository.GetByDocumentoAsync(request.Documento, ct);
            if (existente is not null)
            {
                var atencion = await repository.GetAtencionAsync(existente.PersonaId, request.PsicopedagogoId, ct);

                // El mismo psicopedagogo intenta cargar un paciente que ya atiende: es un intento de duplicado.
                if (atencion is not null && atencion.FechaFin is null)
                {
                    return Result<RegistroPacienteResult>.Failure($"El paciente con documento {request.Documento} ya está registrado en tu lista de pacientes.", ErrorCodes.Duplicate);
                }

                await IniciarOReactivarAtencionAsync(existente.PersonaId, request.PsicopedagogoId, atencion, request.Objetivo, request.Observaciones, ct);

                logger.LogInformation("Paciente existente {Id} vinculado al psicopedagogo {PsicopedagogoId}", existente.PersonaId, request.PsicopedagogoId);
                return Result<RegistroPacienteResult>.Success(new RegistroPacienteResult(PacienteDto.FromEntity(existente), false));
            }

            // El documento existe pero pertenece a alguien que no es paciente (ej. un psicopedagogo).
            if (await personaRepository.IdCardExistAsync(request.Documento, ct: ct))
            {
                return Result<RegistroPacienteResult>.Failure($"El documento {request.Documento} pertenece a otra persona registrada que no es paciente.", ErrorCodes.Duplicate);
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

                PsicopedagogoPacientes = new List<PsicopedagogoPaciente>
                {
                    new()
                    {
                        PsicopedagogoId = request.PsicopedagogoId,
                        FechaInicio = DateTime.Now,
                        Intervencion = new Intervencion
                        {
                            objetivo = request.Objetivo,
                            observaciones = request.Observaciones
                        }
                    }
                }

            };


            await repository.AddPacienteAsync(paciente, ct);

            logger.LogInformation("Paciente creado: {Id} ({documento})", paciente.PersonaId, paciente.Persona.Documento);
            return Result<RegistroPacienteResult>.Success(new RegistroPacienteResult(PacienteDto.FromEntity(paciente), true));
        }

        public async Task<Result<IntervencionDto>> GetIntervencionAsync(int pacienteId, int psicopedagogoId, CancellationToken ct = default)
        {
            var intervencion = await repository.GetIntervencionAsync(pacienteId, psicopedagogoId, ct);

            return intervencion is null
                ? Result<IntervencionDto>.Failure($"El psicopedagogo {psicopedagogoId} no atiende ni atendió al paciente {pacienteId}.", ErrorCodes.NotFound)
                : Result<IntervencionDto>.Success(IntervencionDto.FromEntity(intervencion));
        }

        public async Task<Result<IntervencionDto>> UpdateIntervencionAsync(int pacienteId, int psicopedagogoId, UpsertIntervencionRequest request, CancellationToken ct = default)
        {
            if (!await repository.UpdateIntervencionAsync(pacienteId, psicopedagogoId, request.Objetivo, request.Observaciones, ct))
            {
                return Result<IntervencionDto>.Failure($"El psicopedagogo {psicopedagogoId} no atiende ni atendió al paciente {pacienteId}.", ErrorCodes.NotFound);
            }

            logger.LogInformation("Intervención actualizada: paciente {PacienteId}, psicopedagogo {PsicopedagogoId}", pacienteId, psicopedagogoId);
            return Result<IntervencionDto>.Success(new IntervencionDto(request.Objetivo, request.Observaciones));
        }

        public async Task<Result<PacienteDto>> UpdatePacienteAsync(int pacienteId, UpsertPacienteRequest request, CancellationToken ct = default)
        {
            var paciente = await repository.GetForUpdateAsync(pacienteId, ct);

            if (paciente is null)
            {
                return Result<PacienteDto>.Failure($"El paciente con ID {pacienteId} no fue encontrado.", ErrorCodes.NotFound);
            }

            if (await personaRepository.IdCardExistAsync(request.Documento, excludedId: pacienteId, ct: ct))
            {
                return Result<PacienteDto>.Failure($"El documento {request.Documento} ya pertenece a otra persona.", ErrorCodes.Duplicate);
            }

            paciente.Direccion = request.Direccion;
            paciente.Persona.Nombre = request.Nombre;
            paciente.Persona.Apellido = request.Apellido;
            paciente.Persona.Telefono = request.Telefono;
            paciente.Persona.Documento = request.Documento;
            paciente.Persona.Genero = request.Genero;
            paciente.Persona.FechaNacimiento = request.FechaNacimiento;

            await repository.UpdateAsync(paciente, ct);
            logger.LogInformation("Paciente actualizado: {Id} ({documento})", paciente.PersonaId, paciente.Persona.Documento);
            return Result<PacienteDto>.Success(PacienteDto.FromEntity(paciente));

        }

        /// <summary>Crea la atención con su intervención vacía o inicial; si ya existió y estaba finalizada, la reabre con su mismo historial.</summary>
        private async Task IniciarOReactivarAtencionAsync(int pacienteId, int psicopedagogoId, PsicopedagogoPaciente? atencion, string? objetivo, string? observaciones, CancellationToken ct)
        {
            if (atencion is null)
            {
                await repository.IniciarAtencionAsync(new PsicopedagogoPaciente
                {
                    PacienteId = pacienteId,
                    PsicopedagogoId = psicopedagogoId,
                    FechaInicio = DateTime.Now,
                    Intervencion = new Intervencion
                    {
                        objetivo = objetivo,
                        observaciones = observaciones
                    }
                }, ct);
            }
            else if (atencion.FechaFin is not null)
            {
                await repository.ReactivarAtencionAsync(pacienteId, psicopedagogoId, ct);
            }
        }
    }
}
