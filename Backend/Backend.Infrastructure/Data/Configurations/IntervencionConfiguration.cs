using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Configurations
{
    public class IntervencionConfiguration : IEntityTypeConfiguration<Intervencion>
    {
        public void Configure(EntityTypeBuilder<Intervencion> builder)
        {
            builder.ToTable("Intervencion");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.objetivo).HasMaxLength(50);
            builder.Property(i => i.observaciones).HasMaxLength(100);
        }
    }
}
