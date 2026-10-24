using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class IntervencionTipoDificultadConfiguration : IEntityTypeConfiguration<IntervencionTipoDificultad>
    {
        public void Configure(EntityTypeBuilder<IntervencionTipoDificultad> builder)
        {
            builder.ToTable("IntervencionTipoDificultad");

            builder.HasKey(x => new { x.IntervencionId, x.TipoDificultadId });

            builder.HasOne(x => x.Intervencion)
                .WithMany(i => i.TiposDificultad)
                .HasForeignKey(x => x.IntervencionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Restrict: no se puede borrar del catálogo un tipo de dificultad que ya está en uso.
            builder.HasOne(x => x.TipoDificultad)
                .WithMany(t => t.IntervencionTiposDificultad)
                .HasForeignKey(x => x.TipoDificultadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
