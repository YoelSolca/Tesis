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
        public DbSet<Intervencion> Intervencion => Set<Intervencion>();
        public DbSet<TipoDificultad> TipoDificultad => Set<TipoDificultad>();
        public DbSet<Sesion> Sesion => Set<Sesion>();
        public DbSet<Resultado> Resultado => Set<Resultado>();
        public DbSet<Metrica> Metrica => Set<Metrica>();
        public DbSet<ResultadoMetrica> ResultadoMetrica => Set<ResultadoMetrica>();
        public DbSet<Informe> Informe => Set<Informe>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
