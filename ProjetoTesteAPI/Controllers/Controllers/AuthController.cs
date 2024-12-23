using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoTesteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == request.Email);

            if (client == null || !BCrypt.Net.BCrypt.Verify(request.Password, client.PasswordHash))
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, client.Email),
            new Claim(ClaimTypes.Role, client.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

    [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == request.Email);

            if (existingClient != null)
            {
                return BadRequest("Este e-mail já está em uso.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var client = new Client
            {
                Name = request.Name,
                Email = request.Email,
                CPF = request.CPF,
                Phone = request.Phone,
                Role = "Client",
                PasswordHash = hashedPassword
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return Ok("Cliente registrado com sucesso.");
        }

        public class RegisterRequest
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string CPF { get; set; }
            public string Phone { get; set; }
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}