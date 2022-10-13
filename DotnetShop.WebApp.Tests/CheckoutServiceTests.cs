using DotnetShop.Data.Repositories;
using DotnetShop.Data.Domain;
using DotnetShop.WebApp.Models;
using DotnetShop.WebApp.Services;
using Moq;
using Xunit;

namespace DotnetShop.WebApp.Tests
{
    public class CheckoutServiceTests
    {
        private readonly CheckoutService _checkoutService;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public CheckoutServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _checkoutService = new CheckoutService(_orderRepositoryMock.Object);
        }

        [Fact]
        public void Checkout_ShouldSucceed()
        {
            var customerId = 1;
            _checkoutService.Checkout(customerId, new CartViewModel()
            {
                CustomerId = customerId,
                CartTotal = 200,
                CartLineItemViewModels = new List<CartLineItemViewModel>
                {
                    new CartLineItemViewModel
                    {
                        ProductId = 1,
                        CartLineItemQuantity = 2,
                        ProductName = "Product Name",
                        ProductPrice = 10.0M,
                        CartLineItemTotal = 200,
                    }
                }
            });

            _orderRepositoryMock.Verify(o => o.CreateOrder(It.IsAny<Order>()), Times.Once);
        }
    }
}
