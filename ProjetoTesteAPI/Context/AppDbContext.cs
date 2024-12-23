using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
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

        public class UserSeed
        {
            public static async Task SeedAdminUserAsync(AppDbContext context)
            {
                var admin = await context.Clients
                    .FirstOrDefaultAsync(c => c.Email == "admin@admin.com");

                if (admin == null)
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Senha@123");
                    admin = new Client
                    {
                        Name = "Administrador",
                        Email = "admin@admin.com",
                        CPF = "00000000000", 
                        Phone = "99999999999", 
                        Role = "Admin",
                        PasswordHash = hashedPassword
                    };
                    await context.Clients.AddAsync(admin);
                    await context.SaveChangesAsync();
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}