using Microsoft.AspNetCore.Mvc;
using Movimentos.CrossCutting.Auth.Interface;

namespace Movimentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthController(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("token")]
        public IActionResult GerarToken([FromBody] string usuario)
        {
            var token = _tokenGenerator.GenerateToken(usuario);
            return Ok(new { token });
        }
    }
}