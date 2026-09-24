using Backend.Domain.Entities;

namespace Backend.Application.Interfaces;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}

public interface ITokenGenerator
{
    (string Token, DateTime ExpiresAt) Generate(Usuario usuario);

    (string Token, DateTime ExpiresAt) GenerateResetToken();
}

public interface IUsuarioActual
{
    /// <summary>Id del autor autenticado (claim del JWT), o null si no hay sesión.</summary>
    int? PsicopedagogoId { get; }
}
