using Backend.Application.Common;
using Backend.Application.DTO;


namespace Backend.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<Result<PersonaDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<PersonaDto>> GetAllAsync(CancellationToken ct = default);
        Task<Result<PersonaDto>> CreateAsync(CreatePersonaRequest request, CancellationToken ct = default);
        Task<Result<PersonaDto>> UpdateAsync(int id, UpdatePersonaRequest request, CancellationToken ct = default);
        Task<Result<bool>> DeleteAsync(int id, CancellationToken ct = default);
    }
}
