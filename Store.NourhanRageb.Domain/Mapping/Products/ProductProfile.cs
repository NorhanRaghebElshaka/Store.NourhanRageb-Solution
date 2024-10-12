using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.NourhanRageb.Domain.Dtos.Products;
using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.typeName, options => options.MapFrom(s => s.type.Name))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(new PictureUrlResolver(configuration)));


            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
        }
    }
}
