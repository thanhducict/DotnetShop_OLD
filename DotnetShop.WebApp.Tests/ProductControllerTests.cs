using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.Areas.Admin.Controllers;
using DotnetShop.WebApp.Areas.Admin.Helpers;
using DotnetShop.WebApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Xunit;

namespace DotnetShop.WebApp.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductsController _productController;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IHelpers> _helpersMock;
        private readonly Mock<IFormFile> _formFileMock;

        public ProductControllerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryRepositoryMock.Setup(c => c.GetCategories()).Returns(new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Shirt", Description = "Dotnet shirt."},
                new Category { CategoryId = 2, CategoryName = "Cup", Description = "Dotnet cup."},
                new Category { CategoryId = 3, CategoryName = "Badge", Description = "Dotnet badge."}
            });

            _productRepositoryMock = new Mock<IProductRepository>();
            _productRepositoryMock.Setup(p => p.GetProducts()).Returns(new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    Name="Kudu Purple Sweatshirt",
                    Price = 15.95M,
                    ShortDescription = "Our famous shirt!",
                    LongDescription = "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    Category = _categoryRepositoryMock.Object.GetCategories().ToList()[0],
                    ImageUrl = "Kudu Purple Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "Kudu Purple Sweatshirt.png"
                },
                new Product
                {
                    ProductId = 2,
                    Name=".NET Blue Sweatshirt",
                    Price = 18.95M,
                    ShortDescription = "You'll love it!",
                    LongDescription = "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    Category = _categoryRepositoryMock.Object.GetCategories().ToList()[0],
                    ImageUrl = "NET Blue Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "NET Blue Sweatshirt.png"
                },
                new Product
                {
                    ProductId = 3,
                    Name=".NET Bot Black Sweatshirt",
                    Price = 18.95M,
                    ShortDescription = "Best seller in year.",
                    LongDescription = "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    Category = _categoryRepositoryMock.Object.GetCategories().ToList()[0],
                    ImageUrl = "NET Bot Black Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "NET Bot Black Sweatshirt.png"
                },
            });
            _productRepositoryMock.Setup(p => p.GetProductById(It.IsAny<int>())).Returns(new Product
            {
                ProductId = 2,
                Name = ".NET Blue Sweatshirt",
                Price = 18.95M,
                ShortDescription = "You'll love it!",
                LongDescription = "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                Category = _categoryRepositoryMock.Object.GetCategories().ToList()[0],
                ImageUrl = "NET Blue Sweatshirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "NET Blue Sweatshirt.png"
            });

            _formFileMock = new Mock<IFormFile>();
            _helpersMock = new Mock<IHelpers>();
            _helpersMock.Setup(h => h.UploadFile(_formFileMock.Object, Helpers.UploadFileType.ProductImage, "test.jpg")).Returns(It.IsAny<string>());

            _productController = new ProductsController(_productRepositoryMock.Object, _categoryRepositoryMock.Object, _helpersMock.Object);
        }

        [Fact]
        public void Index_ShouldReturnsViewResult()
        {
            var result = ((ViewResult)_productController.Index()).Model as IEnumerable<Product>;

            Assert.Equal(3, result.Count());
            Assert.Equal(2, result.ElementAt(1).ProductId);
            Assert.Equal(".NET Bot Black Sweatshirt", result.LastOrDefault().Name);
        }

        [Fact]
        public void Details_ShouldReturnsViewResult()
        {
            var result = ((ViewResult)_productController.Details(2)).Model as Product;

            Assert.Equal(2, result.ProductId);
            Assert.Equal(18.95M, result.Price);
        }

        [Fact]
        public void Create_Get_ShouldReturnsViewResult()
        {
            var viewBagCategories = ((ViewResult)_productController.Create()).ViewData.Values.ToList().FirstOrDefault();
            var items = ((IEnumerable<SelectListItem>)viewBagCategories).ToList().OrderBy(i => i.Value);

            var result = ((ViewResult)_productController.Create()).Model as ProductModel;

            Assert.Equal("Shirt", items.FirstOrDefault().Text);
            Assert.Equal("Cup", items.ElementAt(1).Text);
            Assert.Equal("Badge", items.LastOrDefault().Text);

            Assert.Equal(0, result.ProductId);
            Assert.True(string.IsNullOrEmpty(result.Name));
            Assert.Equal(0, result.Price);
        }

        [Fact]
        public void Create_Post_ShouldReturnsRedirect()
        {
            var newProduct = new ProductModel
            {
                ProductId = 1,
                Name = "Kudu Purple Sweatshirt",
                Price = 15.95M,
                ShortDescription = "Our famous shirt!",
                LongDescription = "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "Kudu Purple Sweatshirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "Kudu Purple Sweatshirt.png"
            };

            var result = (RedirectToActionResult)_productController.Create(newProduct);

            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Product", result.ControllerName);
        }

        [Fact]
        public void Edit_Get_ShouldReturnsViewResult()
        {
            var viewBagCategories = ((ViewResult)_productController.Edit(2)).ViewData.Values.ToList().FirstOrDefault();
            var items = ((IEnumerable<SelectListItem>)viewBagCategories).ToList().OrderBy(i => i.Value);

            var result = ((ViewResult)_productController.Edit(2)).Model as ProductModel;

            Assert.Equal("Shirt", items.FirstOrDefault().Text);
            Assert.Equal("Cup", items.ElementAt(1).Text);
            Assert.Equal("Badge", items.LastOrDefault().Text);

            Assert.Equal(2, result.ProductId);
            Assert.Equal(".NET Blue Sweatshirt", result.Name);
            Assert.Equal(18.95M, result.Price);
        }

        [Fact]
        public void Edit_Post_ShouldReturnsRedirect()
        {
            var newProduct = new ProductModel
            {
                ProductId = 2,
                Name = "Edited",
                Price = 10.10M,
                ShortDescription = "Edited!",
                LongDescription = "Edited.",
                CategoryId = 1,
                ImageUrl = "Edited.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "Edited.png"
            };

            var result = (RedirectToActionResult)_productController.Edit(newProduct);

            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Product", result.ControllerName);
        }

        [Fact]
        public void Delete_ShouldReturnsRedirect()
        {
            var result = (RedirectToActionResult)_productController.Delete(1);

            Assert.Equal("Index", result.ActionName);
        }
    }
}
