using System.Text.Json.Serialization;

namespace ProjetoTesteAPI.Models
{
    public class Order : BaseEntity
    {
        public long ClientId { get; set; }
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }
        [JsonIgnore]
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}