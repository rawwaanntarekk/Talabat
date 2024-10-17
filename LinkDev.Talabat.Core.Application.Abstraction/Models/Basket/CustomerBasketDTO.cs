using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Basket
{
    public class CustomerBasketDTO
    {
        public required string Id { get; set; }
        public IEnumerable<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
    }
}
