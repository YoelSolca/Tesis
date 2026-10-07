using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacienteController(IPacienteService pacienteService) : Controller
    {
        /// <summary>Lista paginada de los pacientes del psicopedagogo autenticado (id tomado del JWT).</summary>
        [HttpGet]
        [ProducesResponseType<PagedResponse<PacienteListItemDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int? tipoDificultadId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await pacienteService.GetPacientesAsync(search, tipoDificultadId, page, pageSize, ct);
            return result.IsSuccess ? Ok(result.Value) : ToError(result.Error, result.ErrorCode);
        }

        [HttpGet("{pacienteId:int}")]
        [ProducesResponseType<PacienteDetalleDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int pacienteId, CancellationToken ct)
        {
            var result = await pacienteService.GetDetalleAsync(pacienteId, ct);
            return result.IsSuccess ? Ok(result.Value) : ToError(result.Error, result.ErrorCode);
        }

        /// <summary>
        /// Registra un paciente. Si el documento ya existe no se duplica: se vincula al psicopedagogo con el
        /// paciente existente (200) y empieza su propio historial desde cero. Si es nuevo, responde 201.
        /// Si el psicopedagogo ya atiende a ese paciente, responde 409 (el documento ya está en su lista).
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreatePacienteRequest request, CancellationToken ct)
        {
            var result = await pacienteService.CreatePacienteAsync(request, ct);

            if (!result.IsSuccess)
                return ToError(result.Error, result.ErrorCode);

            var registro = result.Value!;
            return registro.Creado
                ? CreatedAtAction(nameof(GetById), new { pacienteId = registro.Paciente.personaId }, registro.Paciente)
                : Ok(registro.Paciente);
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

        [HttpGet("{pacienteId:int}/psicopedagogos/{psicopedagogoId:int}/intervencion")]
        [ProducesResponseType<IntervencionDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIntervencion(int pacienteId, int psicopedagogoId, CancellationToken ct)
        {
            var result = await pacienteService.GetIntervencionAsync(pacienteId, psicopedagogoId, ct);
            return result.IsSuccess ? Ok(result.Value) : ToError(result.Error, result.ErrorCode);
        }

        [HttpPut("{pacienteId:int}/psicopedagogos/{psicopedagogoId:int}/intervencion")]
        [ProducesResponseType<IntervencionDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateIntervencion(int pacienteId, int psicopedagogoId, UpsertIntervencionRequest request, CancellationToken ct)
        {
            var result = await pacienteService.UpdateIntervencionAsync(pacienteId, psicopedagogoId, request, ct);
            return result.IsSuccess ? Ok(result.Value) : ToError(result.Error, result.ErrorCode);
        }

        private ObjectResult ToError(string? error, string? code)
        {
            var status = code switch
            {
                ErrorCodes.NotFound => StatusCodes.Status404NotFound,
                ErrorCodes.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorCodes.Duplicate => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(detail: error, statusCode: status, title: code);
        }

    }
}
