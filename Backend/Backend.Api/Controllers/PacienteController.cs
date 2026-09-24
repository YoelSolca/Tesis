using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacienteController(IPacienteService pacienteService) : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(await pacienteService.GetAllPacienteAsync(ct));


        [HttpGet("{pacienteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int pacienteId, CancellationToken ct)
        {
            var result = await pacienteService.GetByPacienteIdAsync(pacienteId, ct);
            return result.IsSuccess ? Ok(result.Value) : ToError(result.Error, result.ErrorCode);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreatePacienteRequest request, CancellationToken ct)
        {
            var result = await pacienteService.CreatePacienteAsync(request, ct);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { pacienteId = result.Value!.personaId }, result.Value)
                : ToError(result.Error, result.ErrorCode);
        }

        [HttpPut("{pacienteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int pacienteId, UpsertPacienteRequest request, CancellationToken ct)
        {
            var result = await pacienteService.UpdatePacienteAsync(pacienteId, request, ct);
            return result.IsSuccess
                ? Ok(result.Value)
                : ToError(result.Error, result.ErrorCode);
        }

        private ObjectResult ToError(string? error, string? code)
        {
            var status = code switch
            {
                ErrorCodes.NotFound => StatusCodes.Status404NotFound,
                ErrorCodes.Duplicate => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(detail: error, statusCode: status, title: code);
        }

    }
}
