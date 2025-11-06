using Domain.Contracts;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
            => asNoTracking?await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
            : await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(Tkey id) 
            => await _dbContext.Set<TEntity>().FindAsync(id);


        public async Task AddAsync(TEntity entity) 
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity) 
            => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) 
            => _dbContext.Set<TEntity>().Remove(entity);

        #region specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
           => await SpecificationEvluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications)
            => await SpecificationEvluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications)
           => await SpecificationEvluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();

        #endregion
    }
}
