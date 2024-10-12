using Store.NourhanRageb.Domain.Specifications.Products;
using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Specifications.Products
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(ProductSpecParams productSpec)
            : base(
                  P =>
                  (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
                  &&
                  (!productSpec.BrandId.HasValue || P.Brand.Id == productSpec.BrandId)
                  &&
                  (!productSpec.TypeId.HasValue || P.TypeId == productSpec.TypeId)
            )
        {
            Cretria = null;
            ApplyIncludes();

            if (!string.IsNullOrWhiteSpace(productSpec.sort))
            {
                var result = productSpec.sort.ToLower();
                switch (result)
                {
                    // Name , PriceAsc , PriceDesc
                    case "name":
                        OrderBy = P => P.Name;
                        break;
                    case "priceasc":
                        OrderBy = P => P.Price;
                        break;
                    case "pricedesc":
                        OrderByDescending = P => P.Price;
                        break;
                    default:
                        OrderBy = P => P.Name;
                        break;
                }
            }
            else
            {
                OrderBy = P => P.Name;
            }

            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
        }
        public ProductSpecifications(int id)
        {
            Cretria = P => P.Id == id;
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.type);
        }
    }
}
