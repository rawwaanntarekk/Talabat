using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Domain.Products;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDTO, string?>
	{
		public string? Resolve(Product source, ProductToReturnDTO destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{configuration.GetSection("Urls:ApiBaseUrl")}/{source.PictureUrl}";

			return string.Empty;
		}
	}
}
