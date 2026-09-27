using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class SesionEjercicioConfiguration : IEntityTypeConfiguration<SesionEjercicio>
    {

        public void Configure(EntityTypeBuilder<SesionEjercicio> builder)
        {

            builder.ToTable("SesionEjercicio");

            builder.HasKey(x => new { x.SesionId, x.EjercicioId });

            builder.HasOne(x => x.Sesion)
                .WithMany(x => x.SesionEjercicios)
                .HasForeignKey(x => x.SesionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Restrict: borrar un ejercicio del catálogo no debe borrar en cascada el historial de sesiones
            // que ya lo usaron.
            builder.HasOne(x => x.Ejercicio)
                .WithMany(x => x.SesionEjercicios)
                .HasForeignKey(x => x.EjercicioId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
