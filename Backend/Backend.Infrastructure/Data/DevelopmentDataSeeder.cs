using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Infrastructure.Data
{
    /// <summary>
    /// Datos de prueba solo para desarrollo: 1 psicopedagogo con 3 pacientes, sesiones, resultados e informe.
    /// Es idempotente: si el psicopedagogo ya existe, no hace nada. Los catálogos los siembran las migraciones.
    /// </summary>
    public class DevelopmentDataSeeder(AppDbContext context, IPasswordHasher hasher, ILogger<DevelopmentDataSeeder> logger)
    {
        private const string DocumentoPsicopedagogo = "30111222";
        private const string Obs = "Se observa buena concentración en la primera parte. En la segunda aumentan los errores por distracción. Se recomienda continuar con actividades visuales.";

        // Ids del catálogo sembrado por las migraciones.
        private const int ImagenIgual = 1, Memorama = 4, ConectarPalabras = 5, ComprensionTexto = 6,
                          Dictado = 7, SumasRestas = 8, SeriesNumericas = 9, DesafioNumeros = 10;
        private const int DifAtencion = 1, DifLectoescritura = 2, DifCalculo = 3, DifMemoria = 4;
        private const int Movimientos = 1;

        public async Task SeedAsync(CancellationToken ct = default)
        {
            if (await context.Persona.AnyAsync(p => p.Documento == DocumentoPsicopedagogo, ct)) return;

            var ahora = DateTime.Now;

            var psico = new Psicopedagogo
            {
                Avatar = "avatar-demo.png",
                Persona = NuevaPersona("Laura", "Gómez", "3515551234", DocumentoPsicopedagogo, "F", new DateOnly(1985, 3, 14)),
                Usuario = new Usuario { CorreoElectronico = "psico@demo.com", Contrasenia = hasher.Hash("Demo1234!"), FechaAlta = ahora }
            };

            var juan = NuevoPaciente("Juan", "Perez", "351 45889533", "56058870", "M", new DateOnly(2018, 5, 12), "Av. Colón 559", ahora);
            var melina = NuevoPaciente("Melina", "Romero", "351 4112233", "65221087", "F", new DateOnly(2019, 8, 3), "Bv. San Juan 1020", ahora);
            var cristian = NuevoPaciente("Cristian", "Sosa", "351 4556677", "54576522", "M", new DateOnly(2018, 2, 20), "Obispo Trejo 340", ahora);

            psico.PsicopedagogoPacientes.Add(Vinculo(juan, ahora.AddDays(-30), "Mejorar la lectura comprensiva.",
                "Buena disposición. Se distrae con estímulos auditivos.", [DifAtencion],
                NuevaSesion(ahora.AddDays(-21), EjercicioResuelto(ImagenIgual, 8, 2, 75, 10), EjercicioResuelto(Memorama, 15, 4, 122, 19)),
                NuevaSesion(ahora.AddDays(-14), EjercicioResuelto(DesafioNumeros, 9, 4, 101, 13)),
                NuevaSesion(ahora.AddDays(-7), EjercicioResuelto(ImagenIgual, 10, 1, 68, 11), EjercicioResuelto(Memorama, 17, 3, 110, 21))));

            psico.PsicopedagogoPacientes.Add(Vinculo(melina, ahora.AddDays(-12), "Mejorar la lectoescritura.",
                "Lee con apoyo. Cuesta la escritura de palabras largas.", [DifLectoescritura],
                NuevaSesion(ahora.AddDays(-10), EjercicioResuelto(ComprensionTexto, 6, 3, 180, 0), EjercicioResuelto(ConectarPalabras, 8, 2, 95, 9)),
                NuevaSesion(ahora.AddDays(-3), EjercicioResuelto(Dictado, 7, 3, 140, 0))));

            psico.PsicopedagogoPacientes.Add(Vinculo(cristian, ahora.AddDays(-6), "Afianzar el cálculo mental.",
                "Se frustra ante errores. Responde bien al refuerzo positivo.", [DifCalculo, DifMemoria],
                NuevaSesion(ahora.AddDays(-5), EjercicioResuelto(SumasRestas, 12, 5, 130, 17), EjercicioResuelto(SeriesNumericas, 9, 3, 115, 12))));

            context.Psicopedagogo.Add(psico);
            context.Informe.Add(new Informe
            {
                Paciente = juan,
                Psicopedagogo = psico,
                FechaGeneracion = ahora,
                PeriodoDesde = ahora.AddDays(-30),
                PeriodoHasta = ahora,
                Contenido = "Informe de ejemplo (datos de prueba). Juan muestra mejoras sostenidas en atención y memoria durante el período."
            });

            await context.SaveChangesAsync(ct);
            logger.LogInformation("Datos de desarrollo sembrados (usuario psico@demo.com).");
        }

        private static Persona NuevaPersona(string nombre, string apellido, string telefono, string documento, string genero, DateOnly nacimiento)
            => new()
            {
                Nombre = nombre,
                Apellido = apellido,
                Telefono = telefono,
                Documento = documento,
                Genero = genero,
                FechaNacimiento = nacimiento
            };

        private static Paciente NuevoPaciente(string nombre, string apellido, string telefono, string documento, string genero, DateOnly nacimiento, string direccion, DateTime fechaAlta)
            => new()
            {
                Direccion = direccion,
                FechaAlta = fechaAlta,
                Persona = NuevaPersona(nombre, apellido, telefono, documento, genero, nacimiento)
            };

        private static PsicopedagogoPaciente Vinculo(Paciente paciente, DateTime fechaInicio, string objetivo, string observaciones, int[] dificultades, params Sesion[] sesiones)
            => new()
            {
                Paciente = paciente,
                FechaInicio = fechaInicio,
                Intervencion = new Intervencion
                {
                    objetivo = objetivo,
                    observaciones = observaciones,
                    TiposDificultad = dificultades.Select(id => new IntervencionTipoDificultad { TipoDificultadId = id }).ToList()
                },
                Sesiones = sesiones.ToList()
            };

        private static Sesion NuevaSesion(DateTime fecha, params SesionEjercicio[] ejercicios)
        {
            // Cada ejercicio se registra unos minutos después del anterior.
            for (var i = 0; i < ejercicios.Length; i++)
            {
                ejercicios[i].Resultado!.FechaRegistro = fecha.AddMinutes(10 * (i + 1));
            }

            return new Sesion { Fecha = fecha, SesionEjercicios = ejercicios.ToList() };
        }

        private static SesionEjercicio EjercicioResuelto(int ejercicioId, int aciertos, int errores, int segundos, int movimientos)
            => new()
            {
                EjercicioId = ejercicioId,
                Resultado = new Resultado
                {
                    Aciertos = aciertos,
                    Errores = errores,
                    TiempoSegundos = segundos,
                    Observaciones = Obs,
                    ResultadoMetricas = movimientos > 0
                        ? new List<ResultadoMetrica> { new() { MetricaId = Movimientos, Valor = movimientos } }
                        : new List<ResultadoMetrica>()
                }
            };
    }
}
