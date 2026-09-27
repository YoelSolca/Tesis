using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class MetricaService(IMetricaRepository repository) : IMetricaService
    {
        public async Task<IReadOnlyList<MetricaDto>> GetAllAsync(CancellationToken ct = default)
        {
            var metricas = await repository.GetAllAsync(ct);
            return metricas.Select(MetricaDto.FromEntity).ToList();
        }

        public async Task<Result<MetricaDto>> CreateAsync(CreateMetricaRequest request, CancellationToken ct = default)
        {
            if (await repository.NombreExisteAsync(request.Nombre, ct))
            {
                return Result<MetricaDto>.Failure($"Ya existe una métrica llamada '{request.Nombre}'.", ErrorCodes.Duplicate);
            }

            var metrica = await repository.AddAsync(new Metrica
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Unidad = request.Unidad,
                TipoAgregacion = request.TipoAgregacion
            }, ct);

            return Result<MetricaDto>.Success(MetricaDto.FromEntity(metrica));
        }

        public async Task<Result<bool>> DeleteAsync(int id, CancellationToken ct = default)
        {
            if (!await repository.DeleteAsync(id, ct))
            {
                return Result<bool>.Failure($"La métrica con ID {id} no fue encontrada.", ErrorCodes.NotFound);
            }

            return Result<bool>.Success(true);
        }
    }
}
