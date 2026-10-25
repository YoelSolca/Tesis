using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await authService.LoginAsync(request);

            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(detail: result.Error, statusCode: StatusCodes.Status401Unauthorized, title: result.ErrorCode);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string correoElectronico)
        {
            var result = await authService.ForgotPasswordAsync(correoElectronico);

            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(detail: result.Error, statusCode: StatusCodes.Status401Unauthorized, title: result.ErrorCode);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var result = await authService.ResetPasswordAsync(resetPassword.Token, resetPassword.NuevaContrasenia);
            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(detail: result.Error, statusCode: StatusCodes.Status401Unauthorized, title: result.ErrorCode);
        }
    }
}
