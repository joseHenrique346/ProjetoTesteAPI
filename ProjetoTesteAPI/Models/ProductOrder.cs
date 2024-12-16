namespace ProjetoTesteAPI.Models
{
    public class ProductOrder : BaseEntity
    {
        public ICollection<Product>? ListProduct { get; set; }
    }
}
