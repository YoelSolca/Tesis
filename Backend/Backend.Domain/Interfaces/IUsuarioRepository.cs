using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
        Task UpdateUsuariopAsync(int PsicopedagogoId, string token, DateTime ExpiresAt, CancellationToken ct = default);

        Task ValidatePasswordResetTokenAsync(string token, string nuevaContrasenia, CancellationToken ct = default);

        Task sendEmail(string correoElectronico, CancellationToken ct = default);

    }
}
