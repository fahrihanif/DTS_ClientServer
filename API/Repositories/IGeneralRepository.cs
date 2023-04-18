namespace API.Repositories;

public interface IGeneralRepository<TEntity, TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey key);
    Task<TEntity?> InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<bool> IsExist(TKey key);
}
