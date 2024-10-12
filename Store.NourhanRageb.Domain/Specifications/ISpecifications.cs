using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Specifications
{
    public interface ISpecifications<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity , bool>> Cretria { get; set; }
        public List<Expression<Func<TEntity , object>>> Includes { get; set; }
        public Expression<Func<TEntity , object>> OrderBy { get; set; }
        public Expression<Func<TEntity , object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginatioEnabled { get; set; }
    }
}
