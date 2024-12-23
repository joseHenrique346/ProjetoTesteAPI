using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Models;
using ProjetoTesteAPI.Context;

namespace ProjetoTesteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context; 
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<ActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == request.Email);

            if (client == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            if (client.Role == request.Role)
            {
                return BadRequest("Cliente já tem esta role.");
            }

            client.Role = request.Role;
            await _context.SaveChangesAsync();

            return Ok("Role atribuída com sucesso.");
        }
    }
}