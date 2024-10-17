﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Basket;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(dest => dest.Brand, src => src.MapFrom(product => product.Brand!.Name))
                .ForMember(dest => dest.Category, src => src.MapFrom(product => product.Category!.Name))
                .ForMember(dest => dest.PictureUrl, src => src.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandsDTO>();

            CreateMap<ProductCategory, CategoryDTO>();
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

        }

    }
}
