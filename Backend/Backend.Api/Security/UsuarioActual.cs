using Backend.Application.Interfaces;
using Backend.Infrastructure.Security;

namespace Backend.Api.Security
{
    public class UsuarioActual (IHttpContextAccessor accessor) : IUsuarioActual
    {
        public int? PsicopedagogoId
        {
            get
            {
                var value = accessor.HttpContext?.User.FindFirst(JwtTokenGenerator.PsicopedagogoClaim)?.Value;
                return int.TryParse(value, out var id) ? id : null;
            }
        }
    }
}