using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.Models;

namespace DotnetShop.WebApp.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Checkout(int cusomterId, CartViewModel cartViewModel)
        {
            var orderDetails = cartViewModel.CartLineItemViewModels.Select(o => new OrderDetail
            {
                ProductId = o.ProductId,
                Quantity = o.CartLineItemQuantity,
                ProductName = o.ProductName,
                ProductPrice = o.ProductPrice,
                LineItemTotal = o.CartLineItemTotal,
            }).ToList();

            var order = new Order
            {
                OrderPlaced = DateTime.Now,
                OrderTotal = cartViewModel.CartTotal,
                OrderDetails = orderDetails,
                CustomerId = cusomterId
            };

            _orderRepository.CreateOrder(order);
        }
    }
}
