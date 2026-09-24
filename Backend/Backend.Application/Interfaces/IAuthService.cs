using Backend.Application.Common;
using Backend.Application.DTO;

namespace Backend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);

        Task<Result<ForgotPasswordRequest>> ForgotPasswordAsync(string correoElectronico, CancellationToken ct = default);
        Task<Result<ResetPasswordRequest>> ResetPasswordAsync(string token, string nuevaContrasenia, CancellationToken ct = default);

    }
}
