using DotnetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Data
{
    public class DotnetShopSeed
    {
        /// <summary>
        /// Seed data at runtime.
        /// </summary>
        public static async Task SeedAsync(DotnetShopDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(PopulateSampleCategories());
            }
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(PopulateSampleProducts());
            }

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Category> PopulateSampleCategories()
        {
            return new List<Category> {
                new Category { CategoryId = 1, CategoryName = "Shirt" },
                new Category { CategoryId = 2, CategoryName = "Cup" },
                new Category { CategoryId = 3, CategoryName = "Badge" }
            };
        }

        private static IEnumerable<Product> PopulateSampleProducts()
        {
            return new List<Product> {
                new Product
                {
                    ProductId = 1,
                    Name = "Kudu Purple Sweatshirt",
                    Price = 15.95M,
                    ShortDescription = "Our famous shirt!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/Kudu Purple Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = true,
                    ImageThumbnailUrl = "/images/Products/Kudu Purple Sweatshirt.png"
                },
                new Product
                {
                    ProductId = 2,
                    Name = ".NET Blue Sweatshirt",
                    Price = 18.95M,
                    ShortDescription = "You'll love it!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/NET Blue Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/NET Blue Sweatshirt.png"
                },
                new Product
                {
                    ProductId = 3,
                    Name = ".NET Bot Black Sweatshirt",
                    Price = 18.95M,
                    ShortDescription = "Best seller in year.",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/NET Bot Black Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/NET Bot Black Sweatshirt.png"
                },
                new Product
                {
                    ProductId = 4,
                    Name = ".NET Foundation Sweatshirt",
                    Price = 17.95M,
                    ShortDescription = "A summer classic!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/NET Foundation Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/NET Foundation Sweatshirt.png"
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Prism White T-Shirt",
                    Price = 18.95M,
                    ShortDescription = "Welcome summer!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/Prism White T-Shirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/Prism White T-Shirt.png"
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Roslyn Red T-Shirt",
                    Price = 17.95M,
                    ShortDescription = "A summer favorite.",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/Roslyn Red T-Shirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/Roslyn Red T-Shirt.png"
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Cup<T> T-Shirt",
                    Price = 19.95M,
                    ShortDescription = "Simple the best!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/CupT T-Shirt HD.png",
                    InStock = false,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/CupT T-Shirt.png"
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Cup<T> White Mug",
                    Price = 10.95M,
                    ShortDescription = "A new designed cup.",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 2,
                    ImageUrl = "/images/Products/CupT White Mug HD.png",
                    InStock = true,
                    IsProductOfTheWeek = true,
                    ImageThumbnailUrl = "/images/Products/CupT White Mug.png"
                },
                new Product
                {
                    ProductId = 9,
                    Name = ".NET Black White Mug",
                    Price = 10.95M,
                    ShortDescription = "A classic design cup.",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 2,
                    ImageUrl = "/images/Products/NET Black White Mug HD.png",
                    InStock = true,
                    IsProductOfTheWeek = true,
                    ImageThumbnailUrl = "/images/Products/NET Black White Mug.png"
                },
                new Product
                {
                    ProductId = 10,
                    Name = "Cup<T> Badge",
                    Price = 4.95M,
                    ShortDescription = "A geeky badge!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 3,
                    ImageUrl = "/images/Products/CupT Badge HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/CupT Badge.png"
                },
                new Product
                {
                    ProductId = 11,
                    Name = "NET Foundation Badge",
                    Price = 3.95M,
                    ShortDescription = "You'll love it!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 3,
                    ImageUrl = "/images/Products/NET Foundation Badge HD.png",
                    InStock = false,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/NET Foundation Badge.png"
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Roslyn Red Badge",
                    Price = 3.95M,
                    ShortDescription = "You'll love it!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 3,
                    ImageUrl = "/images/Products/Roslyn Red Badge HD.png",
                    InStock = false,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/Roslyn Red Badge.png"
                }
            };
        }
    }
}
