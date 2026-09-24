using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backend.Application.Services
{
    public class AuthService(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator,
        ILogger<AuthService> logger
        ) : IAuthService
    {
        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            var usuario = await usuarioRepository.GetByEmailAsync(request.CorreoElectronico.Trim().ToLowerInvariant(), ct);

            if(usuario == null || !passwordHasher.Verify(request.Contrasenia, usuario.Contrasenia))
                return Result<LoginResponse>.Failure("Correo electrónico o contraseña incorrectos.", ErrorCodes.InvalidCredentials);

            var (token, expiresAt) = tokenGenerator.Generate(usuario);

            logger.LogInformation("Psicopedagogo{PsicopedagogoId} logueado ", usuario.PsicopedagogoId);

            return Result<LoginResponse>.Success(new LoginResponse(token, expiresAt, usuario.PsicopedagogoId, usuario.Psicopedagogo.Persona.Nombre));
        }

        public async Task<Result<ForgotPasswordRequest>> ForgotPasswordAsync(string correoElectronico, CancellationToken ct = default)
        {
            var existing = await usuarioRepository.GetByEmailAsync(correoElectronico.Trim().ToLowerInvariant(), ct);

            if (existing == null)
                return Result<ForgotPasswordRequest>.Failure("Correo electrónico no encontrado.", ErrorCodes.NotFound);

            var (token, expiresAt) = tokenGenerator.GenerateResetToken();

            await usuarioRepository.UpdateUsuariopAsync(existing.PsicopedagogoId, token, expiresAt, ct);

            logger.LogInformation("Contraseña restablecida para el usuario {PsicopedagogoId}", existing.PsicopedagogoId);

            return Result<ForgotPasswordRequest>.Success(new ForgotPasswordRequest(token, expiresAt));
        }

        public async Task<Result<ResetPasswordRequest>> ResetPasswordAsync(string token, string nuevaContrasenia, CancellationToken ct = default)
        {
            await usuarioRepository.ValidatePasswordResetTokenAsync(token, nuevaContrasenia, ct);

            return Result<ResetPasswordRequest>.Success(new ResetPasswordRequest(token, nuevaContrasenia));
        }
    }
}
