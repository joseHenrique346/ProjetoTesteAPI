using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
             _context = context;
             _dbSet = context.GetDbSet<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public bool Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return true;
        }
    }
}