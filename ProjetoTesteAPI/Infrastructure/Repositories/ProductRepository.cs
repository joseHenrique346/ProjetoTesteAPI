using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context)
    {
    }
}
