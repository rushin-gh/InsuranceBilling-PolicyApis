using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Services.Auth;

namespace APIs.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly LoginService _loginService;

        public AuthController(JwtService jwtService, LoginService loginService)
        {
            _jwtService = jwtService;
            _loginService = loginService;
        }

        [HttpPost("GetJwt")]
        public IActionResult GetJwtToken([FromBody] LoginModel loginModel)
        {  
            // TODO : Username and password verification from db values and encrypted one
            if (_loginService.IsValidUser(loginModel))
            {
                var token = _jwtService.GenerateJwtToken(loginModel);
                return Ok( new { Token = token});
            }
            return Unauthorized("Wrong credentials");
        }
    }
}
