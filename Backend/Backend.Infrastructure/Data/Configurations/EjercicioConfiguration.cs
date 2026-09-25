
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class EjercicioConfiguration : IEntityTypeConfiguration<Ejercicio>
    {
        public void Configure(EntityTypeBuilder<Ejercicio> builder)
        {

            builder.ToTable("Ejercicio");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nombre).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.TipoEjercicio)
                    .WithMany(p => p.Ejercicios)
                    .HasForeignKey(e => e.TipoEjercicioId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
