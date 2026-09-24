using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DTO;

public record AuthDto(int PsicopedagogoId, string CorreoElectronico, string Contrasenia, string? TokenRestablecimiento, DateTime FechaExpiracionTokenRestablecimiento, DateTime FechaRecuperacion)
{
    public static AuthDto FromEntity(Usuario u) => new(u.PsicopedagogoId, u.CorreoElectronico, u.Contrasenia, u.TokenRestablecimiento, u.FechaExpiracionTokenRestablecimiento, u.FechaRecuperacion);
}

public record LoginRequest(
    [Required, EmailAddress] string CorreoElectronico,
    [Required] string Contrasenia);

public record LoginResponse(string Token, DateTime ExpiresAt, int PsicopedagogoId, string PsicopedagogoName);


public record ForgotPasswordRequest(string Token, DateTime ExpiresAt);

public record ResetPasswordRequest(
    [Required] string Token,
    [Required] string NuevaContrasenia);
