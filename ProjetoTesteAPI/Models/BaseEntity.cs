using System.ComponentModel.DataAnnotations;

namespace ProjetoTesteAPI.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; private set; }
        
        public string Name { get; private set; }
        
        public int CodeNumber { get; private set; }
        
        public string Description { get; private set; }
    }
}
