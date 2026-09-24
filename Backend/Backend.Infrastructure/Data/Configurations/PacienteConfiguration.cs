using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Data.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Paciente");
            builder.HasKey(p => p.PersonaId);
            builder.Property(p => p.Direccion).IsRequired().HasMaxLength(50);

            builder.HasOne(p => p.Persona)
                   .WithOne(pa => pa.Paciente)
                   .HasForeignKey<Paciente>(p => p.PersonaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Psicopedagogo)
                    .WithMany(a => a.Pacientes)
                    .HasForeignKey(b => b.PsicopedagogoId)
                    .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
