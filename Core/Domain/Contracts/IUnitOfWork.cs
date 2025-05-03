using Domain.Entities;


namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();

        //Signature for function aill return an instance of class that implement IGenericRepo

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()where TEntity:BaseEntity<TKey>;
    }
}
