using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/ejercicios")]
    public class EjercicioController(IEjercicioService ejercicioService) : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(await ejercicioService.GetAllEjercicioAsync(ct));
    }
}
