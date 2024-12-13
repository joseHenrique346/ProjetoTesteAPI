using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTesteAPI.Models
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Required] //Torna obrigatório informar o preço
        public decimal Price { get; set; }
        [Required]
        [ForeignKey(nameof(Brand))] //Faz uma relação com a tabela "Brands"
        public Brand Brand { get; set; }
    }
}