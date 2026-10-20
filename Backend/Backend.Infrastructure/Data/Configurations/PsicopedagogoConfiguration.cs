using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class PsicopedagogoConfiguration : IEntityTypeConfiguration<Psicopedagogo>
    {
        public void Configure(EntityTypeBuilder<Psicopedagogo> builder)
        {
            builder.ToTable("Psicopedagogo");

            builder.HasKey(p => p.PersonaId);
            builder.Property(p => p.Avatar).HasMaxLength(250);
            builder.Property(p => p.Telefono).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Matricula).IsRequired().HasMaxLength(50);

            builder.HasIndex(p => p.Matricula).IsUnique();

            builder.HasOne(p => p.Persona)
                   .WithOne(ps => ps.Psicopedagogo)
                   .HasForeignKey<Psicopedagogo>(p => p.PersonaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
