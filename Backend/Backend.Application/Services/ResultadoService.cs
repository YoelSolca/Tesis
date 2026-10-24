using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class ResultadoService(
        IResultadoRepository repository,
        IMetricaRepository metricaRepository
        ) : IResultadoService
    {
        public async Task<Result<ResultadoDto>> GetAsync(int sesionId, int ejercicioId, int psicopedagogoId, CancellationToken ct = default)
        {
            if (!await repository.SesionEjercicioPerteneceAsync(sesionId, ejercicioId, psicopedagogoId, ct))
            {
                return Result<ResultadoDto>.Failure(
                    $"El psicopedagogo {psicopedagogoId} no tiene el ejercicio {ejercicioId} en la sesión {sesionId}.", ErrorCodes.Forbidden);
            }

            var resultado = await repository.GetAsync(sesionId, ejercicioId, ct);

            return resultado is null
                ? Result<ResultadoDto>.Failure("Todavía no se registró un resultado para ese ejercicio.", ErrorCodes.NotFound)
                : Result<ResultadoDto>.Success(ResultadoDto.FromEntity(resultado));
        }

        public async Task<Result<ResultadoDto>> UpsertAsync(int sesionId, int ejercicioId, UpsertResultadoRequest request, CancellationToken ct = default)
        {
            if (!await repository.SesionEjercicioPerteneceAsync(sesionId, ejercicioId, request.PsicopedagogoId, ct))
            {
                return Result<ResultadoDto>.Failure(
                    $"El psicopedagogo {request.PsicopedagogoId} no tiene el ejercicio {ejercicioId} en la sesión {sesionId}.", ErrorCodes.Forbidden);
            }

            var metricaIds = (request.Metricas ?? []).Select(m => m.MetricaId).Distinct().ToList();
            if (metricaIds.Count > 0)
            {
                var existentes = await metricaRepository.GetExistingIdsAsync(metricaIds, ct);
                var faltantes = metricaIds.Except(existentes).ToList();
                if (faltantes.Count > 0)
                {
                    return Result<ResultadoDto>.Failure(
                        $"Métrica(s) no encontrada(s): {string.Join(", ", faltantes)}", ErrorCodes.NotFound);
                }
            }

            var resultado = new Resultado
            {
                SesionId = sesionId,
                EjercicioId = ejercicioId,
                Aciertos = request.Aciertos,
                Errores = request.Errores,
                TiempoSegundos = request.TiempoSegundos,
                Observaciones = request.Observaciones,
                FechaRegistro = DateTime.Now,
                ResultadoMetricas = (request.Metricas ?? [])
                    .Select(m => new ResultadoMetrica { MetricaId = m.MetricaId, Valor = m.Valor })
                    .ToList()
            };

            var guardado = await repository.UpsertAsync(resultado, ct);

            // El repositorio devuelve la entidad ya trackeada sin las Metricas cargadas por nombre; se relee para el DTO.
            var completo = await repository.GetAsync(sesionId, ejercicioId, ct);
            return Result<ResultadoDto>.Success(ResultadoDto.FromEntity(completo!));
        }
    }
}
