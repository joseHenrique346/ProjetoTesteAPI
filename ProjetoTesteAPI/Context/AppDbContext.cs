using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Configurations;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}