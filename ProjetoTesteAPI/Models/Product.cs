using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTesteAPI.Models
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public Brand Brand { get; set; }
        public long BrandId { get; set; }
    }
}