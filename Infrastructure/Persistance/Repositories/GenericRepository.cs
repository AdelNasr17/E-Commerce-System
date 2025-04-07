

namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>where TEntity : BaseEntity<TKey>
    {
        private readonly APPDbContext _dbCotext;

        public GenericRepository(APPDbContext DbCotext)
        {
            _dbCotext = DbCotext;
        }

        

        public void Delete(TEntity entity)=> _dbCotext.Set<TEntity>().Remove(entity);
        
        public void Update(TEntity entity) => _dbCotext.Set<TEntity>().Update(entity);
        public async Task AddAsync(TEntity entity)=> await _dbCotext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking) => AsNoTracking ?
            await _dbCotext.Set<TEntity>().AsNoTracking().ToArrayAsync() : //True
            await _dbCotext.Set<TEntity>().ToListAsync();//False
        

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbCotext.Set<TEntity>().FindAsync(id);
        


    }
}
