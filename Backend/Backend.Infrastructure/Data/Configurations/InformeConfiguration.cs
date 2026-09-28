using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class InformeConfiguration : IEntityTypeConfiguration<Informe>
    {
        public void Configure(EntityTypeBuilder<Informe> builder)
        {
            builder.ToTable("Informe");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Contenido).HasColumnType("nvarchar(max)");

            // Restrict: no perder informes ya generados si el paciente se da de baja.
            builder.HasOne(i => i.Paciente)
                .WithMany()
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Restrict: no perder informes ya generados si se borra el psicopedagogo.
            builder.HasOne(i => i.Psicopedagogo)
                .WithMany()
                .HasForeignKey(i => i.PsicopedagogoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.PsicopedagogoId);
        }
    }
}
