using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Apellido).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Telefono).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Documento).IsRequired().HasMaxLength(20).IsRequired();
            builder.Property(p => p.Genero).IsRequired().HasMaxLength(1).IsRequired();
            builder.Property(p => p.FechaNacimiento).IsRequired();

            builder.HasIndex(p => p.Documento).IsUnique();
        }
    }
}
