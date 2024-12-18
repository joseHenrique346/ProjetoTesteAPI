using Microsoft.AspNetCore.Mvc;
using ProjetoTesteAPI.Arguments.Product;
using ProjetoTesteAPI.Controllers.Services;
using ProjetoTesteAPI.DTOs;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Controllers.Controllers
{
    [Route("produtos")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IUnitOfWork _uof;
        public ProductController(IUnitOfWork uof, ProductService productService)
        {
            _productService = productService;
            _uof = uof;
        }

        [HttpGet("Busca de Produtos")]
        public async Task<ActionResult<List<OutputProduct>>> GetAll()
        {
            var result = await _uof.ProductRepository.GetAllAsync();
            return Ok(result.ToListOutputProduct());
        }

        [HttpGet("Busca de Produto por ID")]
        public async Task<ActionResult<OutputProduct>> Get(int id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result is string errorMessage)
            {
                return BadRequest(errorMessage);
            }
            
            if (result != null)
            {
                return NotFound(result);
            } 
            return Ok(result);
        }

        [HttpPost("Criação de Produto")]
        public async Task<ActionResult<OutputProduct>> Create(InputCreateProduct input)
        {
            var product = await _productService.CreateProductAsync(input);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product.ToOutputProduct());
        }

        [HttpPut("Atualização de Produto")]
        public ActionResult<OutputProduct> Update(int id, InputUpdateProduct input)
        {
            var result = _productService.UpdateProduct(id, input);
            return Ok(result.ToOutputProduct());
        }

        [HttpDelete("Removendo Produto")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = _productService.DeleteProductAsync(id);
            return true;
        }
    }
}
