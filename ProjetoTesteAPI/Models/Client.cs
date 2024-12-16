namespace ProjetoTesteAPI.Models
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}