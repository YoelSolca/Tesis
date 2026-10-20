using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/ejercicios")]
    public class EjercicioController(IEjercicioService ejercicioService) : Controller
    {
        [HttpGet]
        [ProducesResponseType<PagedResponse<EjercicioDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] EjercicioSearchCriteria criteria, CancellationToken ct = default)
            => Ok(await ejercicioService.GetAllEjercicioAsync(criteria, ct));
    }
}
