using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class EjercicioService(
            IEjercicioRepository ejercicioRepository
        ) : IEjercicioService
    {
        public async Task<IReadOnlyList<EjercicioDto>> GetAllEjercicioAsync(CancellationToken ct = default)
        {
            var ejercicios = await ejercicioRepository.GetAllEjercicioAsync(ct);

            return ejercicios.Select(EjercicioDto.FromEntity).ToList();
        }
    }
}
