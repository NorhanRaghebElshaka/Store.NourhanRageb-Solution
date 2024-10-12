using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Cretria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginatioEnabled { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
            Cretria = expression;

        }
        public BaseSpecifications()
        {

        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginatioEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
