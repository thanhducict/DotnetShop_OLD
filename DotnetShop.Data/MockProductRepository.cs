using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public class MockProductRepository : IProductRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    Name="Kudu Purple Sweatshirt",
                    Price = 15.95M,
                    ShortDescription = "Our famous shirt!",
                    LongDescription = "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    Category = _categoryRepository.GetCategories().ToList()[0],
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
                    Category = _categoryRepository.GetCategories().ToList()[0],
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
                    Category = _categoryRepository.GetCategories().ToList()[0],
                    ImageUrl = "NET Bot Black Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "NET Bot Black Sweatshirt.png"
                },
            };
        }

        public Product GetProductById(int productId)
        {
            return GetProducts().FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
