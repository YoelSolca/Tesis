using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IEjercicioService 
    {
        Task<PagedResponse<EjercicioDto>> GetAllEjercicioAsync(EjercicioSearchCriteria criteria, CancellationToken ct = default);
    }
}
