using Backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTO;

public record AuthDto(int PersonaId, string CorreoElectronico, string Contrasenia, string? TokenRestablecimiento, DateTime FechaExpiracionTokenRestablecimiento, DateTime FechaRecuperacion)
{
    public static AuthDto FromEntity(Usuario u) => new(u.PersonaId, u.CorreoElectronico, u.Contrasenia, u.TokenRestablecimiento, u.FechaExpiracionTokenRestablecimiento, u.FechaRecuperacion);
}

public record LoginRequest(
    [Required, EmailAddress] string CorreoElectronico,
    [Required] string Contrasenia);

public record LoginResponse(string Token, DateTime ExpiresAt, int PsicopedagogoId, string PsicopedagogoName);


public record ForgotPasswordRequest(string Token, DateTime ExpiresAt);

public record ResetPasswordRequest(
    [Required] string Token,
    [Required] string NuevaContrasenia);
