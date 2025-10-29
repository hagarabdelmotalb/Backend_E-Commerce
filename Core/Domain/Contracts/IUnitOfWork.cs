using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity,Tkey> GetRepository<TEntity,Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
