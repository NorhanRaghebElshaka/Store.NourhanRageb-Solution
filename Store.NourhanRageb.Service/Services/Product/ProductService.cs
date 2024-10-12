using AutoMapper;
using Store.NourhanRageb.Domain;
using Store.NourhanRageb.Domain.Dtos.Products;
using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Services.Contract;
using Store.NourhanRageb.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Service.Services.Products
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductSpecParams productSpec;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
            => _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repositroy<ProductType, int>().GetAllAsync());
        public async Task<ProductDto> GetProudctById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repositroy<Product,int>().GetWithSpecsAsync(spec);
            //var product = await _unitOfWork.Repositroy<Product, int>().GetAsync(id);
            var mappedproduct = _mapper.Map<ProductDto>(product);
            return mappedproduct;
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repositroy<ProductBrand, int>().GetAllAsync();
            var mappedBrands = _mapper.Map<IEnumerable<TypeBrandDto>>(brands);
            return mappedBrands;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);

            return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repositroy<Product, int>().GetAllWithSpecsAsync(spec));
            // return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repositroy<Product, int>().GetAllAsync());
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
