using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class InformeService(IInformeRepository repository) : IInformeService
    {
        public async Task<Result<IReadOnlyList<InformeDto>>> GetAllAsync(int psicopedagogoId, CancellationToken ct = default)
        {
            var informes = await repository.GetAllAsync(psicopedagogoId, ct);
            return Result<IReadOnlyList<InformeDto>>.Success(informes.Select(InformeDto.FromEntity).ToList());
        }

        public async Task<Result<InformeDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var informe = await repository.GetByIdAsync(id, ct);
            return informe is null
                ? Result<InformeDto>.Failure("El informe no existe.", ErrorCodes.NotFound)
                : Result<InformeDto>.Success(InformeDto.FromEntity(informe));
        }

        public async Task<Result<InformeDto>> CreateAsync(int pacienteId, CreateInformeRequest request, CancellationToken ct = default)
        {
            if (!await repository.PacientePerteneceAsync(pacienteId, request.PsicopedagogoId, ct))
            {
                return Result<InformeDto>.Failure(
                    $"El psicopedagogo {request.PsicopedagogoId} no atiende al paciente {pacienteId}.", ErrorCodes.Forbidden);
            }

            // TODO: reemplazar por la generación real con IA (paso siguiente, no incluido en esta etapa).
            var contenido = $"Informe generado automáticamente para el período {request.PeriodoDesde:d} - {request.PeriodoHasta:d}.";

            var informe = new Informe
            {
                PacienteId = pacienteId,
                PsicopedagogoId = request.PsicopedagogoId,
                FechaGeneracion = DateTime.Now,
                PeriodoDesde = request.PeriodoDesde,
                PeriodoHasta = request.PeriodoHasta,
                Contenido = contenido
            };

            var creado = await repository.AddAsync(informe, ct);
            return Result<InformeDto>.Success(InformeDto.FromEntity(creado));
        }
    }
}
