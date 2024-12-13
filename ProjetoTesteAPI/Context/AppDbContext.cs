using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Brands { get; set; }
        public DbSet<Brand> Products { get; set; }
    }
}
