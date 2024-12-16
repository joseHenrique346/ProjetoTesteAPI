using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTesteAPI.Models
{
    [Table("Brands")]
    public class Brand: BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual List<Product>? ListProduct { get; set; }
    }
}
