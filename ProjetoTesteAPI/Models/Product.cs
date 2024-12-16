using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTesteAPI.Models
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long BrandId { get; set; }
        public decimal Price { get; set; }

        public virtual Brand Brand { get; set; }
    }
}