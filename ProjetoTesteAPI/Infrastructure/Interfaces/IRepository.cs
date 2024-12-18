namespace ProjetoTesteAPI.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(long id);
        Task<T> CreateAsync(T entity);
        bool Update(T entity);
        Task<bool> DeleteAsync(long id);

        //List<T> GetAll();
        //T? Get(long id);
        //T Create(T entity);
        //bool Delete(long id);
    }
}