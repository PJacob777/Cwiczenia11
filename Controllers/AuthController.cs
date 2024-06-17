using Microsoft.AspNetCore.Mvc;
using JWT.RequestModels;
using JWT.Services;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await authService.LoginAsync(loginRequest);
            if (response != null)
            {
                return Ok(response);
            }
            return Unauthorized("Wrong username or password");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var response = await authService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);
            if (response != null)
            {
                return Ok(response);
            }
            return Unauthorized("Invalid token");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await authService.RegisterAsync(registerRequest);
            if (success)
            {
                return Ok("User registered successfully");
            }
            return BadRequest("Username already exists");
        }
    }
}