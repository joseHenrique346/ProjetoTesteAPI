using System.ComponentModel.DataAnnotations;

namespace ProjetoTesteAPI.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        public int CodeNumber { get; set; }
        [StringLength(160)]
        public string Description { get; set; }
    }
}
