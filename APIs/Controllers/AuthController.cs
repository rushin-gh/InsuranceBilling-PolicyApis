using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Services.Authentication;

namespace APIs.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("GenerateJwt")]
        public IActionResult GetJwtToken([FromBody] LoginModel loginModel)
        {  
            // TODO : Username and password verification from db values and encrypted one
            if (true)
            {
                var token = _jwtService.GenerateJwtToken(loginModel);
                return Ok( new { Token = token});
            }
            return Unauthorized("No access");
        }
    }
}
