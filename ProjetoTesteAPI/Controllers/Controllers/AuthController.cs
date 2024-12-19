using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;   

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (!_authService.IsValidClient(login.Email, login.Password))
                return Unauthorized(new { Message = "Credenciais inválidas." });

            var client = _authService.GetClientByEmail(login.Email);
            if (client == null)
                return Unauthorized(new { Message = "Cliente não encontrado." });

            var token = _jwtService.GenerateJwtToken(client);
            return Ok(new { Token = token });
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel register)
        {
            _authService.RegisterClient(register.Name, register.Email, register.Password, register.CPF, register.Phone);
            return Ok(new { Message = "Cliente registrado com sucesso!" });
        }

        [HttpPut("assign-role")]
        [Authorize(Roles = "admin")]
        public IActionResult AssignRole([FromBody] AssignRoleModel model)
        {
            var client = _authService.GetClientByEmail(model.Email);
            if (client == null)
                return NotFound(new { Message = "Cliente não encontrado." });

            client.Role = model.Role;
            _authService.UpdateClient(client);

            return Ok(new { Message = "Role atribuída com sucesso!" });
        }

    }
}
