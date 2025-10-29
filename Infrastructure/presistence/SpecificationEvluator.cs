using Domain.Contracts;

namespace Persistence
{
    internal class SpecificationEvluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(
            IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications
        ) where TEntity : Domain.Entities.BaseEntity<TKey>
        {
            var query = inputQuery;

            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            if (specifications.IncludeExpression != null && specifications.IncludeExpression.Any())
                query = specifications.IncludeExpression.Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));

            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);

            else if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            return query;
        }
    }
}
