using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(dest => dest.Brand, options => options.MapFrom(product => product.Brand!.Name))
                .ForMember(dest => dest.Category, options => options.MapFrom(product => product.Category!.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandsDTO>();

            CreateMap<ProductCategory, CategoryDTO>();

            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(O => O.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(OI => OI.Product.ProductItemId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(OI => OI.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureURLResolver>());

            CreateMap<Address, AddressDTO>();

            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
                



        }

    }
}
