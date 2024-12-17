using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context) : Repository<Order>(context)
    {
    }
}