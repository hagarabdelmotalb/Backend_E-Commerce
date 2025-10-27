using Domain.Contracts;
using Domain.Entities;
using Persistence.Data;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly StoreDbContext _dbContext;
        public readonly ConcurrentDictionary<string, object> _repositories = new();
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }
        public IGenericRepository<TEntity, Tkey> GenericRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
            =>(IGenericRepository<TEntity, Tkey>) _repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, Tkey>(_dbContext));

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
