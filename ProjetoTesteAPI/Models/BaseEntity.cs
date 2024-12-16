using System.ComponentModel.DataAnnotations;

namespace ProjetoTesteAPI.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
    }
}