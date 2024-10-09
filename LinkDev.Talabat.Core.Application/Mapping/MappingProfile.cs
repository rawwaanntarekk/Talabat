using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Employees;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(dest => dest.Brand, src => src.MapFrom(product => product.Brand!.Name))
                .ForMember(dest => dest.Category, src => src.MapFrom(product => product.Category!.Name));

            CreateMap<ProductBrand, BrandsDTO>();

            CreateMap<ProductCategory, CategoryDTO>();

            CreateMap<Employee , EmployeeToReturnDTO>()
                  .ForMember(dest => dest.Department, src => src.MapFrom(employee => employee.Department!.Name));
        }

    }
}
