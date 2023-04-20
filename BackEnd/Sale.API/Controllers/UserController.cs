using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Domain.DTO;

namespace Sale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(UserDTO model)
        {
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDTO model)
        {
            return Ok();
        }
    }
}
