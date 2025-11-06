using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class BaseSecifications<TEntity, TKey>
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region criteria
        protected BaseSecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; } 
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = new();

        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        } 
        #endregion

        #region OrderBy and OrderByDescending
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression) => OrderByDescending = orderByDescExpression;


        #endregion

        #region Pagination
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void ApplyPaging(int pagesSize, int pageIndex)
        {
            IsPagingEnabled = true;
            Take = pagesSize;
            Skip = Math.Max(0, (pageIndex - 1) * pagesSize);
        }
        #endregion
    }
}
