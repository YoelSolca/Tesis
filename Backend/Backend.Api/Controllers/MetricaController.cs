using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/metricas")]
    public class MetricaController(IMetricaService metricaService) : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
            => Ok(await metricaService.GetAllAsync(ct));

        [HttpPost]
        [ProducesResponseType<MetricaDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateMetricaRequest request, CancellationToken ct)
        {
            var result = await metricaService.CreateAsync(request, ct);
            return result.IsSuccess ? StatusCode(StatusCodes.Status201Created, result.Value) : ToError(result.Error, result.ErrorCode);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await metricaService.DeleteAsync(id, ct);
            return result.IsSuccess ? NoContent() : ToError(result.Error, result.ErrorCode);
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
