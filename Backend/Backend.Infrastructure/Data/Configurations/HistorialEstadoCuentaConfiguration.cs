using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class HistorialEstadoCuentaConfiguration : IEntityTypeConfiguration<HistorialEstadoCuenta>
    {
        public void Configure(EntityTypeBuilder<HistorialEstadoCuenta> builder)
        {
            builder.ToTable("HistorialEstadoCuenta");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.EstadoAnterior).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(h => h.EstadoNuevo).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(h => h.Motivo).HasMaxLength(500);
            builder.Property(h => h.Fecha).IsRequired();

            builder.HasOne(h => h.Usuario)
                   .WithMany(u => u.Historial)
                   .HasForeignKey(h => h.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Restrict: el registro de auditoría no debe desaparecer si se borra al administrador
            // (y evita dos caminos de borrado en cascada desde Persona).
            builder.HasOne(h => h.Administrador)
                   .WithMany(a => a.CambiosRealizados)
                   .HasForeignKey(h => h.AdministradorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(h => h.UsuarioId);
        }
    }
}
