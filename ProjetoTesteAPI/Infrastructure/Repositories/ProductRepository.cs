using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Models;
using System.Linq.Expressions;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<Product?> GetWithIncludesAsync(long id, params Expression<Func<Product, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
