using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.PsicopedagogoId);
            builder.Property(u => u.CorreoElectronico)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Contrasenia)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.FechaExpiracionTokenRestablecimiento);
            builder.Property(u => u.FechaRecuperacion);

            builder.HasOne(p => p.Psicopedagogo)
                  .WithOne(ps => ps.Usuario)
                  .HasForeignKey<Usuario>(p => p.PsicopedagogoId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
