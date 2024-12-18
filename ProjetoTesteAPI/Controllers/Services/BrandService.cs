using Microsoft.AspNetCore.Mvc;
using ProjetoTesteAPI.Arguments.Brand;
using ProjetoTesteAPI.DTOs;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Controllers.Services
{
    public class BrandService
    {
        private readonly IUnitOfWork _uof;
        public BrandService(IUnitOfWork uof)
        {
             _uof = uof;
        }

        public async Task<string?> ValidateGetBrandAsync(int id)
        {
            var existingId = (await _uof.BrandRepository.GetAsync(id));
            if (existingId is null)
            {
                return "*ERRO* Tem certeza que digitou o ID certo?";
            }

            return null;
        }

        public async Task<object> GetBrandAsync(int id)
        {
            var validationMessage = await ValidateGetBrandAsync(id);
            if (validationMessage != null)
            {
                return validationMessage;
            }

            var brand = await _uof.BrandRepository.GetAsync(id);

            return brand;
        }

        public async Task<string?> ValidateCreateBrandAsync(InputCreateBrand input)
        {
            var existingBrand = (await _uof.BrandRepository.GetAllAsync())
                                .FirstOrDefault(x => x.Name
                                .Equals(input.Name, 
                                StringComparison.OrdinalIgnoreCase));

            if (existingBrand != null)
            {
                return "Este nome já está em uso!";
            }

            var existingCode = (await _uof.BrandRepository.GetAllAsync())
                                .FirstOrDefault(x => 
                                x.Code.Equals(input.Code));

            if (existingCode != null)
            {
                return "Este código já está em uso!";
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return "O código tem que ser preenchido!";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição tem que ser preenchida!";
            }

            return null;
        }

        public async Task<Brand> CreateBrandAsync(InputCreateBrand input)
        {
            var validationMessage = await ValidateCreateBrandAsync(input);
            if (validationMessage != null)
            {
                throw new Exception(validationMessage);
            }

            var brand = await _uof.BrandRepository.CreateAsync(input.ToBrand());
            await _uof.CommitAsync();
            return brand;
        }

        public string? ValidateUpdateBrand(int id, InputUpdateBrand input)
        {
            var currentBrand = _uof.BrandRepository.GetAsync(id);

            if (currentBrand == null)
            {
                return "A marca especificada não foi encontrada.";
            }

            var allBrands = _uof.BrandRepository.GetAll();

            var existingNameBrand = allBrands.FirstOrDefault(x =>
                x.Name.Equals(input.Name, StringComparison.OrdinalIgnoreCase) &&
                x.Id != currentBrand.Id);

            if (existingNameBrand != null)
            {
                return "Já existe uma marca com este nome.";
            }

            var existingCodeBrand = allBrands.FirstOrDefault(x =>
                x.Code.Equals(input.Code) &&
                x.Id != currentBrand.Id);

            if (existingCodeBrand != null)
            {
                return "Já existe uma marca com este código.";
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return "A descrição não pode ser vazia.";
            }

            return null;
        }



        public Brand UpdateBrand(int id, InputUpdateBrand input)
        {
            var validationMessage = ValidateUpdateBrand(id, input);
            if (validationMessage is string)
            {
                throw new Exception(validationMessage);
            }

            var newBrand = new Brand
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description
            };

            _uof.BrandRepository.Update(newBrand);
            _uof.CommitAsync();

            return newBrand;
        }

        public async Task<string?> ValidateDeleteBrandAsync(int id)
        {
            var existingBrand = (await _uof.BrandRepository.GetAllAsync())
                                .FirstOrDefault(x => x.Id == id);

            if (existingBrand is null)
            {
                return "Não foi encontrado o ID inserido, foi informado corretamente?";
            }

            return null;
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var validationMessage = await ValidateDeleteBrandAsync(id);
            if (validationMessage is string)
            {
                throw new Exception(validationMessage);
            }

            await _uof.BrandRepository.DeleteAsync(id);
            await _uof.CommitAsync();
            return true;
        }
    }
}