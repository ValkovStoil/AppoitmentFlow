using AppoitmentFlow.API.DTOs.Auth;
using AppoitmentFlow.API.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AppoitmentFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register( RegisterRequestDTO request)
        {
            var result = await _authService.RegisterAsync(request);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginRequestDTO request)
        {
            var result = await _authService.LoginAsync(request);

            return Ok(result);
        }
    }
}
