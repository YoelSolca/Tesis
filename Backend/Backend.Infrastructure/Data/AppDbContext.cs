using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Persona> Persona => Set<Persona>();
        public DbSet<Psicopedagogo> Psicopedagogo => Set<Psicopedagogo>();
        public DbSet<Paciente> Paciente => Set<Paciente>();
        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Ejercicio> Ejercicio => Set<Ejercicio>();
        public DbSet<TipoEjercicio> TipoEjercicio => Set<TipoEjercicio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
