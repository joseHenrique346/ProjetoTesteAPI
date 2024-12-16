namespace ProjetoTesteAPI.Models
{
    public class Order : BaseEntity
    {
        public long ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ProductOrder? ProductOrder { get; set; }
    }
}