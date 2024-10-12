using Microsoft.EntityFrameworkCore;
using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Repository
{
   public class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
   {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery; 

            if (spec.Cretria is not null)
            {
                query = query.Where(spec.Cretria);
            }

            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            } if (spec.IsPaginatioEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            spec.Includes.Aggregate(query , (currentQuery , includeException) => currentQuery.Include(includeException));                                    
            return query;
        }
   }
}
