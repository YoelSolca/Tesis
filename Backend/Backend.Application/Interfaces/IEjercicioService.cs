using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IEjercicioService 
    {
        Task<IReadOnlyList<EjercicioDto>> GetAllEjercicioAsync(CancellationToken ct = default);
    }
}
