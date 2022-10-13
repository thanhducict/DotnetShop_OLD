using DotnetShop.Data.Domain;
using DotnetShop.Data.Repositories;
using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace DotnetShop.WebApp.Tests
{
    public class CartServiceTests
    {
        private readonly CartService _cartService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public CartServiceTests()
        {
            var customer = new Customer
            {
                Cart = "{\"CartLineItems\":[{\"ProductId\":10,\"Quantity\":2}]}"
            };

            _productRepositoryMock = new Mock<IProductRepository>();
            _productRepositoryMock.Setup(p => p.GetProducts()).Returns(new List<Product>
            {
                new Product { ProductId = 1, Price = 10M },
                new Product { ProductId = 10, Price = 50M }
            });

            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerRepositoryMock.Setup(c => c.Update(customer)).Returns(customer);

            var applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser {
                    UserName = "gohan",
                    Customer = customer
                },
            };
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object,
                null, null, null, null, null, null, null, null);
            _userManagerMock.Setup(u => u.Users).Returns(applicationUsers.AsQueryable());

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextAccessorMock.Setup(req => req.HttpContext.User.Identity.Name).Returns("gohan");

            _cartService = new CartService(_productRepositoryMock.Object, _customerRepositoryMock.Object,
                _userManagerMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public void GetCart_ShouldReturnsResult()
        {
            var expected = new Cart
            {
                CartLineItems = new List<CartLineItem>
                {
                    new CartLineItem { ProductId = 10, Quantity = 2 },
                },
            };

            var cart = _cartService.GetCart();

            Assert.Equal(expected.CartLineItems[0].ProductId, cart.CartLineItems[0].ProductId);
            Assert.Equal(expected.CartLineItems[0].Quantity, cart.CartLineItems[0].Quantity);
        }

        [Fact]
        public void AddToCart_ShouldSucceed()
        {
            var productId = 1;
            var quantity = 20;

            _cartService.AddToCart(productId, quantity);
            var cart = _cartService.GetCart();

            Assert.Equal(productId, cart.CartLineItems[1].ProductId);
            Assert.Equal(quantity, cart.CartLineItems[1].Quantity);
        }

        [Fact]
        public void RemoveFromCart_ShouldSucceed()
        {
            var productId = 10;

            _cartService.RemoveFromCart(productId);
            var cart = _cartService.GetCart();

            Assert.Single(cart.CartLineItems);
        }

        [Fact]
        public void ClearCart_ShouldSucceed()
        {
            _cartService.ClearCart();
            var cart = _cartService.GetCart();

            Assert.Empty(cart.CartLineItems);
        }

        [Fact]
        public void GetCartTotal_ShouldReturnsResult()
        {
            var total = _cartService.GetCartTotal();

            Assert.Equal(100, total);
        }

        [Fact]
        public void GetCartLineItemsQuantity_ShouldReturnsResult()
        {
            var lineItemsQuantity = _cartService.GetCartLineItemsQuantity();

            Assert.Equal(1, lineItemsQuantity);
        }
    }
}
