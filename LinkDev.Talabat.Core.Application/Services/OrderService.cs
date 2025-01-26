using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Common.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class OrderService(IBasketService _basketService, IUnitOfWork _unitOfWork, IMapper _mapper) : IOrderService
    {
        private async Task<ICollection<OrderItem>> GetOrderItems(CustomerBasketDTO basket)
        {
            var orderItems = new List<OrderItem>();
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id);

                if (product is not null)
                {
                    var productItemOrdered = new ProductItemOrdered()
                    {
                        ProductItemId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl ?? ""
                    };

                    var orderItem = new OrderItem()
                    {
                        Product = productItemOrdered,
                        Price = product.Price,
                        Quantity = item.Quantity
                    };

                    orderItems.Add(orderItem);

                }

            }

            return orderItems;


        }

        public async Task<OrderToReturnDTO> CreateOrderAsync(string buyerEmail, OrderToCreateDTO order)
        {
            // Work flow
            // 1. Get Basket From Baskets Repo
            var basket = await _basketService.GetCustomerBasketAsync(order.BasketId);

            // 2. Get Selected Items at Basket From Products Repo
            ICollection<OrderItem> orderItems;

            if (basket!.Items.Count() > 0)
                orderItems = (ICollection<OrderItem>)GetOrderItems(basket);
            else
                orderItems = Array.Empty<OrderItem>();


            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            // 3. Calculate Subtotal


            // 4. Mapping Address DTO To Adress 

            var address = _mapper.Map<Address>(order.ShippingAddress);
            // 5. Create Order

            var orderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                Items = orderItems,
                Subtotal = subTotal,
                DeliveryMethodId = order.DeliveryMethodId,

            };

            await _unitOfWork.GetRepository<Order, int>().AddAsync(orderToCreate);

            // 6. Save the order to the database

            var created = await _unitOfWork.CompleteAsync() > 0;

            if (!created)
                throw new BadRequestException("an error has occured during creating your order");

            return _mapper.Map<OrderToReturnDTO>(orderToCreate);

        }

        public async Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDTO>>(deliveryMethods);

        }

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpecifications = new OrderSpecifications(buyerEmail , orderId);
            var order = await _unitOfWork.GetRepository<Order, int>().GetAsyncWithSpec(orderSpecifications);

            if (order is null) throw new NotFoundException(nameof(Order) , orderId);

            return _mapper.Map<OrderToReturnDTO>(order);


        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetOrderForUserAsync(string buyerEmail)
        {
            var orderSpecifications = new OrderSpecifications(buyerEmail);

            var orders = await _unitOfWork.GetRepository<Order, int>().GetAllAsyncWithSpec(orderSpecifications);

            return _mapper.Map<IEnumerable<OrderToReturnDTO>>(orders);
        }
    }
}
