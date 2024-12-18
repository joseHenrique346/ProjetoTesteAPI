using Microsoft.AspNetCore.Mvc;
using ProjetoTesteAPI.Arguments.Product;
using ProjetoTesteAPI.DTOs;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Controllers.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _uof;
        public ProductService(IUnitOfWork uof)
        {
             _uof = uof;
        }

        public async Task<string?> ValidateGetProductAsync(int id)
        {
            var existingProduct = (await _uof.ProductRepository.GetAsync(id));
            if (existingProduct is null)
            {
                return "*ERRO* Tem certeza que digitou o ID certo?";
            }

            return null;              
        }

        public async Task<object> GetProductAsync(int id)
        {
            var validationMessage = await ValidateGetProductAsync(id);
            if (validationMessage != null)
            {
                throw new Exception(validationMessage);
            }

            var product = _uof.ProductRepository.GetAsync(id);
            return product;
        }

        public async Task<string?> ValidateCreateProductAsync(InputCreateProduct input)
        {
            var existingProduct = (await _uof.ProductRepository.GetAllAsync())
                                  .FirstOrDefault(x => x.Code.Equals(input.Code));

            if (existingProduct != null)
            {
                return "Já existe produto com este Código.";
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return "O código tem que ser preenchido!";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição tem que ser preenchida!";
            }

            if (input.Stock < 0)
            {
                return "O estoque não pode ser menor que zero.";
            }

            if (input.Price < 0)
            {
                return "O preço não pode ser menor que zero.";
            }

            if (input.BrandId <= 0)
            {
                return "Informe um valor válido para o Id";
            }

            return null;
        }

        public async Task<Product> CreateProductAsync(InputCreateProduct input)
        {
            var validationMessage = await ValidateCreateProductAsync(input);
            if (validationMessage != null)
            {
                throw new Exception(validationMessage);
            }

            
            var product = await _uof.ProductRepository.CreateAsync(input.ToProduct());
            await _uof.CommitAsync();
            return product;
        }

        public string ValidateUpdateProduct(int id, InputUpdateProduct input)
        {
            var allProducts = _uof.ProductRepository.GetAll();
            var currentProduct = _uof.ProductRepository.GetAsync(id);

            var existingNameProduct = allProducts
                                    .FirstOrDefault(x =>
                                    x.Name.Equals(input.Name,
                                    StringComparison.OrdinalIgnoreCase) &&
                                    x.Id != currentProduct.Id);

            if (existingNameProduct != null)
            {
                return "Já existe uma marca com este nome.";
            }

            var existingCodeProduct = allProducts
                                     .FirstOrDefault(x =>
                                     x.Code.Equals(input.Code) &&
                                     x.Id != currentProduct.Id);

            if (existingCodeProduct != null)
            {
                return "Já existe uma marca com este código.";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição não pode ser vazia.";
            }

            return null;
        }

        public Product UpdateProduct(int id, InputUpdateProduct input)
        {
            var validationMessage = ValidateUpdateProduct(id, input);

            if (validationMessage is string)
            {
                throw new Exception(validationMessage);
            }

            var newProduct = new Product
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description
            };

            _uof.ProductRepository.Update(newProduct);
            _uof.Commit();
            return newProduct;
        } 

        public async Task<string?> ValidateDeleteProductAsync(int id)
        {
            var existingProduct = (await _uof.ProductRepository.GetAllAsync())
                                  .FirstOrDefault(x => x.Id.Equals(id));
            if (existingProduct is null)
            {
                return "Não foi encontrado o ID inserido, foi informado corretamente?";
            }

            return null;
        }

        public async Task <bool> DeleteProductAsync(int id)
        {
            var validationMessage = await ValidateDeleteProductAsync(id);
            if (validationMessage is string)
            {
                throw new Exception(validationMessage);
            }

            await _uof.ProductRepository.DeleteAsync(id);
            await _uof.CommitAsync();
            return true;
        }
    }
}
