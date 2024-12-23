using Microsoft.EntityFrameworkCore;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Models;
using System.Linq.Expressions;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    public async Task<TEntity?> GetAsync(long id, Expression<Func<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (include != null)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id);
    }

    public TEntity? Get(long id)
    {
        return _dbSet.Find(id);
    }

    public List<TEntity?> GetAll()
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

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public bool Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        return true;
    }
}