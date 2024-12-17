using ProjetoTesteAPI.Models;
using System.Text.Json.Serialization;

namespace ProjetoTesteAPI.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long clientId, List<ProductOrder> productList)
    {
         public long ClientId { get; } = clientId;
         [JsonIgnore]
         public DateTime CreatedDate { get; } = DateTime.Now.Date;
         public List<ProductOrder> ProductList { get; } = productList;
    }
}