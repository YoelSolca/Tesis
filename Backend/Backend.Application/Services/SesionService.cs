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
        IEjercicioRepository ejercicioRepository,
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

            var ejercicioIds = sesion.EjercicioIds.Distinct().ToList();
            var faltantes = await Faltantes(ejercicioIds, ct);
            if (faltantes.Count > 0)
            {
                return Result<SesionDto>.Failure(
                    $"Ejercicio(s) no encontrado(s): {string.Join(", ", faltantes)}", ErrorCodes.NotFound);
            }

            var newSesion = new Sesion
            {
                PacienteId = sesion.PacienteId,
                PsicopedagogoId = sesion.PsicopedagogoId,
                Fecha = DateTime.Now,
                SesionEjercicios = ejercicioIds.Select(id => new SesionEjercicio { EjercicioId = id }).ToList()
            };

            await sesionRepository.AddAsync(newSesion, ct);

            logger.LogInformation("Sesion creada: {Id} ({fecha})", newSesion.Id, newSesion.Fecha);

            return Result<SesionDto>.Success(SesionDto.FromEntity(newSesion));
        }

        public async Task<Result<SesionDto>> UpdateAsync(int id, int psicopedagogoId, UpsertSesionRequest sesion, CancellationToken ct = default)
        {
            var ejercicioIds = sesion.EjercicioIds.Distinct().ToList();
            var faltantes = await Faltantes(ejercicioIds, ct);
            if (faltantes.Count > 0)
            {
                return Result<SesionDto>.Failure(
                    $"Ejercicio(s) no encontrado(s): {string.Join(", ", faltantes)}", ErrorCodes.NotFound);
            }

            // Suma los ejercicios nuevos a los que la sesión ya tenía; no borra ni reemplaza nada.
            var updated = await sesionRepository.AgregarEjerciciosAsync(id, psicopedagogoId, ejercicioIds, ct);
            if (updated is null)
            {
                return Result<SesionDto>.Failure($"La sesión con ID {id} no fue encontrada.", ErrorCodes.NotFound);
            }

            logger.LogInformation("Sesion actualizada: {Id}", id);
            return Result<SesionDto>.Success(SesionDto.FromEntity(updated));
        }

        private async Task<List<int>> Faltantes(List<int> ejercicioIds, CancellationToken ct)
        {
            var existentes = await ejercicioRepository.GetExistingIdsAsync(ejercicioIds, ct);
            return ejercicioIds.Except(existentes).ToList();
        }
    }
}
