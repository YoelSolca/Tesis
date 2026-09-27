using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class ResultadoMetricaConfiguration : IEntityTypeConfiguration<ResultadoMetrica>
    {
        public void Configure(EntityTypeBuilder<ResultadoMetrica> builder)
        {
            builder.ToTable("ResultadoMetrica");

            builder.HasKey(rm => new { rm.SesionId, rm.EjercicioId, rm.MetricaId });
            builder.Property(rm => rm.Valor).HasPrecision(10, 2);

            builder.HasOne(rm => rm.Resultado)
                .WithMany(r => r.ResultadoMetricas)
                .HasForeignKey(rm => new { rm.SesionId, rm.EjercicioId })
                .OnDelete(DeleteBehavior.Cascade);

            // Restrict: no se puede borrar del catálogo una métrica que ya tiene valores registrados.
            builder.HasOne(rm => rm.Metrica)
                .WithMany(m => m.ResultadoMetricas)
                .HasForeignKey(rm => rm.MetricaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
