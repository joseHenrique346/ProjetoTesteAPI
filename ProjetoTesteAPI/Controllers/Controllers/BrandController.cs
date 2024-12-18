using Microsoft.AspNetCore.Mvc;
using ProjetoTesteAPI.Arguments.Brand;
using ProjetoTesteAPI.Controllers.Services;
using ProjetoTesteAPI.DTOs;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Controllers.Controllers
{
    [Route("marca")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _uof;

        public BrandController(IUnitOfWork uof, BrandService brandService)
        {
            _brandService = brandService;
             _uof = uof;
        }

        [HttpGet("Busca de Marcas")]
        public async Task<ActionResult<List<OutputBrand>>> GetAll()
        {
            var brand =  await _uof.BrandRepository.GetAllAsync();
            return Ok(brand.ToListOutputBrand());
        }

        [HttpGet("Busca de Marca por ID")]
        public async Task<ActionResult<OutputBrand>> Get(int id)
        {
            var result = await _brandService.GetBrandAsync(id);
            if (result is string errorMessage)
            {
                return BadRequest(errorMessage);
            }
            var brand = result as Brand;
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand.ToOutputBrand());
        }

        [HttpPost("Criação de Marca")]
        public async Task<ActionResult<OutputBrand>> CreateAsync(InputCreateBrand input)
        {
            var brand = await _brandService.CreateBrandAsync(input);
            return CreatedAtAction(nameof(CreateAsync), new { id = brand.Id }, brand.ToOutputBrand());
        }

        [HttpPut("Atualização de Marca")]
        public ActionResult<OutputBrand> Update(int id, InputUpdateBrand input)
        {
            var result = _brandService.UpdateBrand(id, input); 
            return Ok(result.ToOutputBrand());
        }

        [HttpDelete("Removendo Marca")]
        public ActionResult<bool> Delete(int id)
        {
            var result = _brandService.DeleteBrandAsync(id);
            return Ok(result);
        }
    }
}
