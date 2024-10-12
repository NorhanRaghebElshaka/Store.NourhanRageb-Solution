using Store.NourhanRageb.Domain.Dtos.Products;
using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Services.Contract
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec);
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProudctById(int id);
    }
}
