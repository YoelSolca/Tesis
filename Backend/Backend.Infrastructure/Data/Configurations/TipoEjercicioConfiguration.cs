using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class TipoEjercicioConfiguration : IEntityTypeConfiguration<TipoEjercicio>
    {
        public void Configure(EntityTypeBuilder<TipoEjercicio> builder)
        {
            builder.ToTable("TipoEjercicio");
            builder.HasKey(t => t.Id);

            // El valor de icono es un marcador: hoy el Front elige el ícono por nombre de tipo.
            builder.HasData(
                new TipoEjercicio { Id = 1, Nombre = "Atención", icono = "atencion" },
                new TipoEjercicio { Id = 2, Nombre = "Memoria", icono = "memoria" },
                new TipoEjercicio { Id = 3, Nombre = "Lectura", icono = "lectura" },
                new TipoEjercicio { Id = 4, Nombre = "Escritura", icono = "escritura" },
                new TipoEjercicio { Id = 5, Nombre = "Cálculo", icono = "calculo" });
        }
    }
}
