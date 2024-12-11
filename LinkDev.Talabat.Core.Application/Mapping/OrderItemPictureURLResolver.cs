using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal class OrderItemPictureURLResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
	{
		public string Resolve(OrderItem source, OrderItemDTO destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.PictureUrl))
				return $"{configuration.GetSection("Urls:ApiBaseUrl")}/{source.Product.PictureUrl}";

			return string.Empty;
		}
	}
}
