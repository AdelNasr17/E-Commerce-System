
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {

        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking);

        public Task AddAsync(TEntity entity);

       public void Update(TEntity entity);

        public void Delete(TEntity entity);

    }
}
