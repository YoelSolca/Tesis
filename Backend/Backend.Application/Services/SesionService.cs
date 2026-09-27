using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backend.Application.Services
{
    public class SesionService(
        ISesionRepository sesionRepository,
        IPacienteRepository pacienteRepository,
        ILogger<SesionService> logger
        ) : ISesionService
    {
        public async Task<IReadOnlyList<SesionDto>> GetAllAsync(int psicopedagogoId, int? pacienteId = null, CancellationToken ct = default)
        {
            var sesiones = await sesionRepository.GetAllAsync(psicopedagogoId, pacienteId, ct);

            return sesiones.Select(SesionDto.FromEntity).ToList();
        }

        public async Task<Result<SesionDto>> GetByIdAsync(int id, int psicopedagogoId, CancellationToken ct = default)
        {
            var sesion = await sesionRepository.GetByIdAsync(id, psicopedagogoId, ct);

            return sesion is null
                ? Result<SesionDto>.Failure($"La sesión con ID {id} no fue encontrada.", ErrorCodes.NotFound)
                : Result<SesionDto>.Success(SesionDto.FromEntity(sesion));
        }
        public async Task<Result<SesionDto>> AddAsync(CreateSesionRequest sesion, CancellationToken ct = default)
        {
            var atencion = await pacienteRepository.GetAtencionAsync(sesion.PacienteId, sesion.PsicopedagogoId, ct);

            if (atencion is null || atencion.FechaFin is not null)
            {
                return Result<SesionDto>.Failure(
                    $"El psicopedagogo {sesion.PsicopedagogoId} no atiende actualmente al paciente {sesion.PacienteId}.",
                    ErrorCodes.Forbidden);
            }

            var newSesion = new Sesion
            {
                PacienteId = sesion.PacienteId,
                PsicopedagogoId = sesion.PsicopedagogoId,
                Fecha = DateTime.Now,
            };

            await sesionRepository.AddAsync(newSesion, ct);

            logger.LogInformation("Sesion creada: {Id} ({fecha})", newSesion.Id, newSesion.Fecha);


            return Result<SesionDto>.Success(SesionDto.FromEntity(newSesion));
        }


        public async Task<Result<SesionDto>> UpdateAsync(UpsertSesionRequest sesion, CancellationToken ct = default)
        {

            throw new NotImplementedException();
            //if( await sesionRepository.GetByIdAsync(sesion.Id, ct) is null)
            //{
            //    return Result<SesionDto>.Failure($"La sesión con ID {sesion.Id} no fue encontrada.", ErrorCodes.NotFound);
            //}

            //var updatedSesion = new Sesion
            //{
            //    PacienteId = sesion.PacienteId,
            //    Fecha = sesion.Fecha,
            //};

            //await sesionRepository.UpdateAsync(updatedSesion, ct);

            //logger.LogInformation("Sesion actualizada: {Id} ({fecha})", updatedSesion.Id, updatedSesion.Fecha);
            //return Result<SesionDto>.Success(SesionDto.FromEntity(updatedSesion));
        }
    }
}
