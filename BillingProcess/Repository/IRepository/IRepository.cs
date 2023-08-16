namespace BillingProcess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();

    }
}
