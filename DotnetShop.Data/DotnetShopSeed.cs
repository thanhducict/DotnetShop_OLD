using DotnetShop.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotnetShop.Data
{
    public class DotnetShopSeed
    {
        /// <summary>
        /// Seed data at runtime.
        /// </summary>
        public static async Task SeedAsync(
            DotnetShopDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger logger)
        {
            await context.Database.EnsureCreatedAsync();

            try
            {
                // This won't run because Categories data was added during Model Seed
                if (!await context.Categories.AnyAsync())
                {
                    await context.Categories.AddRangeAsync(PopulateSampleCategories());
                    await context.SaveChangesAsync();
                }
                // Products data will be added at runtime
                if (!await context.Products.AnyAsync())
                {
                    await context.Products.AddRangeAsync(PopulateSampleProducts());
                    await context.SaveChangesAsync();
                }

                if (!await roleManager.RoleExistsAsync(Constants.AdminRole))
                {
                    var adminRole = new IdentityRole { Name = Constants.AdminRole };
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync(Constants.CustomerRole))
                {
                    var customerRole = new IdentityRole { Name = Constants.CustomerRole };
                    await roleManager.CreateAsync(customerRole);
                }

                if (await userManager.FindByNameAsync("trunks") == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "trunks",
                        Email = "trunks@example.com"
                    };

                    var result = await userManager.CreateAsync(user, "Cvbnm123@");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Constants.AdminRole);
                    }
                }

                if (await userManager.FindByNameAsync("gohan") == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "gohan",
                        Email = "gohan@example.com",
                        Customer = new Customer
                        {
                            CustomerName = "Son Gohan",
                            Gender = true,
                            Address = "JP"
                        }
                    };

                    var result = await userManager.CreateAsync(user, "Cvbnm123@");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Constants.CustomerRole);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        private static IEnumerable<Category> PopulateSampleCategories()
        {
            return new List<Category> {
                new Category { CategoryName = "Shirt" },
                new Category { CategoryName = "Cup" },
                new Category { CategoryName = "Badge" }
            };
        }

        private static IEnumerable<Product> PopulateSampleProducts()
        {
            return new List<Product> {
                // identity_insert is set to off so there will be no ProductId
                new Product
                {
                    Name = "Kudu Purple Sweatshirt",
                    Price = 15.95M,
                    ShortDescription = "Our famous shirt!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/Kudu Purple Sweatshirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = false,
                    ImageThumbnailUrl = "/images/Products/Kudu Purple Sweatshirt.png"
                },
                new Product
                {
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
                    Name = "Prism White T-Shirt",
                    Price = 18.95M,
                    ShortDescription = "Welcome summer!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 1,
                    ImageUrl = "/images/Products/Prism White T-Shirt HD.png",
                    InStock = true,
                    IsProductOfTheWeek = true,
                    ImageThumbnailUrl = "/images/Products/Prism White T-Shirt.png"
                },
                new Product
                {
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
                    Name = "Roslyn Red Badge",
                    Price = 3.95M,
                    ShortDescription = "You'll love it!",
                    LongDescription =
                        "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                    CategoryId = 3,
                    ImageUrl = "/images/Products/Roslyn Red Badge HD.png",
                    InStock = false,
                    IsProductOfTheWeek = true,
                    ImageThumbnailUrl = "/images/Products/Roslyn Red Badge.png"
                }
            };
        }

    }
}
