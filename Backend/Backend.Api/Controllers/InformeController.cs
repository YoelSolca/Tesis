using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/informes")]
    public class InformeController(IInformeService informeService) : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int psicopedagogoId, CancellationToken ct)
        {
            var result = await informeService.GetAllAsync(psicopedagogoId, ct);
            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType<InformeDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await informeService.GetByIdAsync(id, ct);
            return result.IsSuccess ? Ok(result.Value) : ToError(result.Error, result.ErrorCode);
        }

        [HttpPost("~/api/pacientes/{pacienteId:int}/informes")]
        [ProducesResponseType<InformeDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(int pacienteId, CreateInformeRequest request, CancellationToken ct)
        {
            var result = await informeService.CreateAsync(pacienteId, request, ct);
            return result.IsSuccess ? StatusCode(StatusCodes.Status201Created, result.Value) : ToError(result.Error, result.ErrorCode);
        }

        private ObjectResult ToError(string? error, string? code)
        {
            var status = code switch
            {
                ErrorCodes.NotFound => StatusCodes.Status404NotFound,
                ErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(detail: error, statusCode: status, title: code);
        }
    }
}
