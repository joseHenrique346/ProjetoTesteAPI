using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public class BrandRepository(AppDbContext context) : Repository<Brand>(context)
    {
    }
}
