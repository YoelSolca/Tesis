
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

            builder.HasData(
                new Ejercicio { Id = 1, Nombre = "Encontrá la imagen igual", TipoEjercicioId = 1 },
                new Ejercicio { Id = 2, Nombre = "Sombras y animales", TipoEjercicioId = 1 },
                new Ejercicio { Id = 3, Nombre = "Clasificar colores", TipoEjercicioId = 1 },
                new Ejercicio { Id = 4, Nombre = "Memorama", TipoEjercicioId = 2 },
                new Ejercicio { Id = 5, Nombre = "Conectar palabras", TipoEjercicioId = 3 },
                new Ejercicio { Id = 6, Nombre = "Comprensión de texto", TipoEjercicioId = 3 },
                new Ejercicio { Id = 7, Nombre = "Dictado de palabras", TipoEjercicioId = 4 },
                new Ejercicio { Id = 8, Nombre = "Sumas y restas", TipoEjercicioId = 5 },
                new Ejercicio { Id = 9, Nombre = "Series numéricas", TipoEjercicioId = 5 },
                new Ejercicio { Id = 10, Nombre = "Desafío de números", TipoEjercicioId = 5 });
        }
    }
}
