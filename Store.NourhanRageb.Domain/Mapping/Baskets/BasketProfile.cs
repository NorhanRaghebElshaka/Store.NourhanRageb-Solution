using AutoMapper;
using Store.NourhanRageb.Domain.Dtos.Baskets;
using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket , CustomerBasketDto>().ReverseMap();
        }
    }
}
