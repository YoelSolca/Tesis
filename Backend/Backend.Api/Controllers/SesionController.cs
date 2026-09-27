using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{

    [ApiController]
    [Route("api/sesiones")]
    [AllowAnonymous]
    public class SesionController(ISesionService sesionService) : Controller
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int psicopedagogoId, [FromQuery] int? pacienteId, CancellationToken ct)
        => Ok(await sesionService.GetAllAsync(psicopedagogoId, pacienteId, ct));


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, [FromQuery] int psicopedagogoId)
        {
            var result = await sesionService.GetByIdAsync(id, psicopedagogoId);

            return result.IsSuccess ? Ok(result) : ToError(result.Error, result.ErrorCode);

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateSesionRequest request)
        {
            var result = await sesionService.AddAsync(request);

            return result.IsSuccess 
                ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id, psicopedagogoId = result.Value.PsicopedagogoId }, result.Value)
                : ToError(result.Error, result.ErrorCode);
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, UpsertSesionRequest request)
        {
            var result = await sesionService.UpdateAsync(request);

            return result.IsSuccess 
                ? Ok(result) 
                : ToError(result.Error, result.ErrorCode);
        }

        private ObjectResult ToError(string? error, string? code)
        {
            var status = code switch
            {
                ErrorCodes.NotFound => StatusCodes.Status404NotFound,
                ErrorCodes.Duplicate => StatusCodes.Status409Conflict,
                ErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(detail: error, statusCode: status, title: code);
        }
    }
}
