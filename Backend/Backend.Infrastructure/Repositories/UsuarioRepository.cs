using Azure.Core;
using Backend.Application.Common;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class UsuarioRepository(
        AppDbContext context,
                IPasswordHasher contraseniaHasher) : IUsuarioRepository
    {
        public async Task<Usuario?> GetByEmailAsync(string correoElectronico, CancellationToken ct = default)
         => await context.Usuario.AsNoTracking()
            .Include(x => x.Psicopedagogo)
            .Include(x => x.Psicopedagogo.Persona)
            .FirstOrDefaultAsync(u => u.CorreoElectronico == correoElectronico, ct);

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        => await context.Usuario.AsNoTracking().AnyAsync(u => u.CorreoElectronico == email, ct);



        public async Task UpdateUsuariopAsync(int psicopedagogoId, string token, DateTime ExpiresAt, CancellationToken ct = default)
        {
            var existingPsicopedagogo = await context.Usuario.FirstOrDefaultAsync(p => p.PsicopedagogoId == psicopedagogoId, ct);

            if (existingPsicopedagogo == null) return;

            existingPsicopedagogo.TokenRestablecimiento = token;
            existingPsicopedagogo.FechaExpiracionTokenRestablecimiento = ExpiresAt;

            await context.SaveChangesAsync(ct);
        }

        public async Task ValidatePasswordResetTokenAsync(string token, string nuevaContrasenia, CancellationToken ct = default)
        {
            var existingUsuario = await
                   context.Usuario
                   .FirstOrDefaultAsync(u => u.TokenRestablecimiento == token && u.FechaExpiracionTokenRestablecimiento > DateTime.Now);

            if (existingUsuario == null) return;

            existingUsuario.Contrasenia = contraseniaHasher.Hash(nuevaContrasenia);
            existingUsuario.TokenRestablecimiento = null;
            existingUsuario.FechaExpiracionTokenRestablecimiento = DateTime.MinValue;
            existingUsuario.FechaRecuperacion = DateTime.Now;

           await context.SaveChangesAsync(ct);
        }
    }
}
