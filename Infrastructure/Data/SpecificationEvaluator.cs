using System.Linq;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // Evaluates BaseSpecification.cs expressions and then creates query from it
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)        // come in as a db set from generic repository therefore we can apply db context methods to apply queries into expression
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);  // spec.Criteria equivalent to p => p.ProductTypeId == id
            }

            // Sorting logic to create a query
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);  
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // Pagination logic to create a query
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}