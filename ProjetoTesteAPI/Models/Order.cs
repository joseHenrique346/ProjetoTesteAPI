namespace ProjetoTesteAPI.Models
{
    public class Order : BaseEntity
    {
        public long ClientId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}