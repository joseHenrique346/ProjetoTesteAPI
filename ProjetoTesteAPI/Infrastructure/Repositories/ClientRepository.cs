using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public class ClientRepository(AppDbContext context) : Repository<Client>(context)
    {
    }
}