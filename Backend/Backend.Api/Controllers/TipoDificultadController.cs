using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/tipos-dificultad")]
    public class TipoDificultadController(ITipoDificultadQueries queries) : Controller
    {
        /// <summary>Catálogo de tipos de dificultad, para el filtro de la lista y el alta de pacientes.</summary>
        [HttpGet]
        [ProducesResponseType<IReadOnlyList<TipoDificultadDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(await queries.GetAllAsync(ct));
    }
}
