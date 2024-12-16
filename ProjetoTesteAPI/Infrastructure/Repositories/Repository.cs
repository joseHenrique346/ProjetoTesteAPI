using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
             _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity? Get(long id)
        {
            return _dbSet.Find(id);
        }

        public bool Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public bool Delete(long id)
        {
            _dbSet.Remove(new TEntity { Id = id});
            return true;
        }
    }
}