using AuthenticationAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
        [ApiController]
        [Route("api/users")]
        [Authorize]
        public class UsersController : ControllerBase
        {
            private readonly IUserService _service;
            public UsersController(IUserService service)
            {
                _service = service;
            }
            [HttpGet]
            [Authorize(Roles = "Administrador")]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _service.GetAllAsync());
            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(string id)
            {
                var user = await _service.GetByIdAsync(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            [HttpDelete("{id}")]
            [Authorize(Roles = "Administrador")]
            public async Task<IActionResult> Delete(string id)
            {
                return Ok(await _service.DeleteAsync(id));
            }
        }
}
