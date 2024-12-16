namespace ProjetoTesteAPI.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? Get(long id);
        T Create(T entity);
        bool Update(T entity);
        bool Delete(long id);
    }
}