using AutoMapper;
using LinkDev.Talabat.Core.Domain.Products;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
