using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class TipoDificultadConfiguration : IEntityTypeConfiguration<TipoDificultad>
    {
        public void Configure(EntityTypeBuilder<TipoDificultad> builder)
        {
            builder.ToTable("TipoDificultad");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Nombre).IsRequired().HasMaxLength(50);

            builder.HasIndex(t => t.Nombre).IsUnique();

            builder.HasData(
                new TipoDificultad { Id = 1, Nombre = "Dificultades de atención" },
                new TipoDificultad { Id = 2, Nombre = "Dificultades en lectoescritura" },
                new TipoDificultad { Id = 3, Nombre = "Dificultades en cálculo" },
                new TipoDificultad { Id = 4, Nombre = "Dificultades en memoria" });
        }
    }
}
