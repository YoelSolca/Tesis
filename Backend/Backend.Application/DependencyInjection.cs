using Backend.Application.Interfaces;
using Backend.Application.Options;
using Backend.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IPsicopedagogoService, PsicopedagogoService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            return services;
        }
    }
}
