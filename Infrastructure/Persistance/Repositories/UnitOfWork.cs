

namespace Persistance.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly APPDbContext _dbContext;
        private ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(APPDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }
        public async Task<int> SaveChangesAsync()=> await _dbContext.SaveChangesAsync();
       
        
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //var typeName= typeof(TEntity).Name;
            // if(_repositories.ContainsKey(typeName))          
            //     return (IGenericRepository<TEntity, TKey>) _repositories[typeName];

            // else
            // {
            //     var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            //     _repositories.Add(typeName, repo);
            //     return repo;
            // }

            return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name  ,
                (_) => new GenericRepository<TEntity, TKey>(_dbContext));
        
        }
    }
}
