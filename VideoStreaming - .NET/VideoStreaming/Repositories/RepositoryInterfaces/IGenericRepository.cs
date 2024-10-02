namespace VideoStreaming.Repositories.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
