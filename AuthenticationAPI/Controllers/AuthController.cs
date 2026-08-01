using AuthenticationAPI.DTO;
using AuthenticationAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.RegisterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
        var result = await _service.LoginAsync(dto);

        if(!result.Success)
        {
            return Unauthorized(result);
        }
        return Ok(result);
    }


        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Id = User.FindFirst("sub")?.Value,
                Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                       ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value,
                Nombre = User.Identity?.Name,
                Roles = User.Claims
                    .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                    .Select(c => c.Value)
            });
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(id))
                return Unauthorized();

            await _service.LogoutAsync(id);

            return Ok("Sesion cerrada");
        }

    }
}