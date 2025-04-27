using AdminDashboard.Models.Products;
using AutoMapper;
using LinkDev.Talabat.Core.Domain.Products;

namespace AdminDashboard.Helpers
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
