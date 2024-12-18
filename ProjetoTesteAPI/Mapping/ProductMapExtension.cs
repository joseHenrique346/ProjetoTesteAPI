using ProjetoTesteAPI.Arguments.Product;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.DTOs
{
    public static class ProductMapExtension
    {
        public static OutputProduct ToOutputProduct(this Product product)
        {
            return new OutputProduct(product.Id, product.Name, product.Description, product.Code, product.Price, product.BrandId, product.Stock);
        }

        public static Product ToProduct(this InputCreateProduct input)
        {
            return new Product
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description
            };
        }

        public static Product ToProduct(this InputUpdateProduct input)
        {
            return new Product
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description
            };
        }

        public static List<OutputProduct> ToListOutputProduct(this List<Product> brand)
        {
            return brand.Select(x => new OutputProduct(x.Id, x.Name, x.Description, x.Code, x.Price, x.BrandId, x.Stock)).ToList();
        }
    }
}