using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Specifications.Products
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product , int>
    {
        public ProductWithCountSpecifications(ProductSpecParams productSpec)
          : base
          (
                P =>
                (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
                &&
                (!productSpec.BrandId.HasValue || P.Brand.Id == productSpec.BrandId)
                && (!productSpec.TypeId.HasValue || P.TypeId == productSpec.TypeId)
          )
        {
        }
    }
}
