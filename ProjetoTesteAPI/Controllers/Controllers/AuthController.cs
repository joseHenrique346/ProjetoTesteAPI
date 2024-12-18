using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (_authService.IsValidClient(login.Email, login.Password))
            {
                var token = JwtTokenGenerator.GenerateToken(login.Email, _configuration); 
                return Ok(new { Token = token });
            }

            return Unauthorized(); 
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel register)
        {
            _authService.RegisterClient(register.Name, register.Email, register.Password, register.CPF, register.Phone);
            return Ok(new { Message = "Cliente registrado com sucesso!" });
        }
    }
}
