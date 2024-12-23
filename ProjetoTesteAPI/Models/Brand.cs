using ProjetoTesteAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
[Table("marcas")]
public class Brand : BaseEntity
{
    public long Id { get; set; }  
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public virtual List<Product>? ListProduct { get; set; }
}