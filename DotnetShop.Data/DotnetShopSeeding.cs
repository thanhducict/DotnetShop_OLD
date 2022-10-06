using DotnetShop.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotnetShop.Data
{
    public class DotnetShopSeeding
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

                if (!await context.Warehouses.AnyAsync())
                {
                    await context.Warehouses.AddRangeAsync(PopulateSampleWarehouses());
                    await context.SaveChangesAsync();
                }

                if (!await context.ProductWarehouses.AnyAsync())
                {
                    await AddProductsToWarehouses(context);
                    await context.SaveChangesAsync();
                }

                await AddSampleRoles(roleManager);

                await AddSampleUsers(userManager);

                if (!await context.Orders.AnyAsync() && !await context.OrderDetails.AnyAsync())
                {
                    await context.Orders.AddRangeAsync(PopulateSampleOrders());
                    await context.SaveChangesAsync();
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

        public static IEnumerable<Product> PopulateSampleProducts()
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
                    ImageThumbnailUrl = "/images/Products/Kudu Purple Sweatshirt.png",
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

        public static IEnumerable<Warehouse> PopulateSampleWarehouses()
        {
            return new List<Warehouse> {
                new Warehouse
                {
                    WarehouseName = "New York",
                    Address = "20 W 34th St., New York, NY 10001, United States",
                    Latitude = "40.748773",
                    Longitude = "-73.985717",
                },
                new Warehouse
                {
                    WarehouseName = "San Francisco",
                    Address = "65 Crestline Dr, San Francisco, CA 94131, United States",
                    Latitude = "37.751316",
                    Longitude = "-122.446492",
                },
                new Warehouse
                {
                    WarehouseName = "Los Angeles",
                    Address = "700 W 7th St, Los Angeles, California, United States",
                    Latitude = "34.047913",
                    Longitude = "-118.259088",
                },

                new Warehouse
                {
                    WarehouseName = "Redmond",
                    Address = "3720 159th Ave NE, Redmond, WA 98052",
                    Latitude = "47.644649",
                    Longitude = "-122.124576",
                },
                new Warehouse
                {
                    WarehouseName = "Temecula",
                    Address = "28544 Old Town Front St, Temecula, California, United States",
                    Latitude = "33.49477",
                    Longitude = "-117.1502",
                },
                new Warehouse
                {
                    WarehouseName = "San Diego",
                    Address = "7077 Friars Rd, San Diego, California, United States",
                    Latitude = "32.770029",
                    Longitude = "-117.163378",
                },
                new Warehouse
                {
                    WarehouseName = "Austin",
                    Address = "1619 E Cesar Chavez St, Austin, Texas, United Statesm",
                    Latitude = "30.257724",
                    Longitude = "-97.727186",
                },
                new Warehouse
                {
                    WarehouseName = "New Hampshire",
                    Address = "837 2nd St, Manchester, New Hampshire, United States",
                    Latitude = "42.970134",
                    Longitude = "-71.480045",
                },
                new Warehouse
                {
                    WarehouseName = "Boston",
                    Address = "218 Market St, Boston, Massachusetts, United States",
                    Latitude = "42.35507",
                    Longitude = "-71.149556",
                },
                new Warehouse
                {
                    WarehouseName = "Michigan",
                    Address = "1389 Walker Village Dr NW, Walker, Michigan, United States",
                    Latitude = "42.990265",
                    Longitude = "-85.754184",
                },
            };
        }

        public static async Task AddProductsToWarehouses(DotnetShopDbContext context)
        {
            var skipRandom = new Random();
            var quantityRandom = new Random();
            var allProducts = context.Products.ToList();
            var allWarehouses = context.Warehouses.ToList();

            foreach (var product in allProducts)
            {
                foreach (var warehouse in allWarehouses)
                {
                    // skip adding some products to warehouses
                    var r = skipRandom.Next(1, 3);
                    if (r == 2)
                    {
                        continue;
                    }

                    await context.ProductWarehouses.AddAsync(new ProductWarehouse
                    {
                        ProductId = product.ProductId,
                        WarehouseId = warehouse.WarehouseId,
                        ProductQuantity = quantityRandom.Next(50, 500)
                    });
                }
            }
        }

        private static async Task AddSampleRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Constants.Roles.AdminRole))
            {
                var adminRole = new IdentityRole { Name = Constants.Roles.AdminRole };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(Constants.Roles.CustomerRole))
            {
                var customerRole = new IdentityRole { Name = Constants.Roles.CustomerRole };
                await roleManager.CreateAsync(customerRole);
            }
        }

        private static async Task AddSampleUsers(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByNameAsync("trunks") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "trunks",
                    Email = "trunks@example.com",
                    Level = Constants.Levels.Leadership,
                };

                var result = await userManager.CreateAsync(user, "Cvbnm123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.AdminRole);
                }
            }

            if (await userManager.FindByNameAsync("wonderwoman") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "wonderwoman",
                    Email = "wonderwoman@example.com",
                    Level = Constants.Levels.Management,
                };

                var result = await userManager.CreateAsync(user, "Cvbnm123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.AdminRole);
                }
            }

            if (await userManager.FindByNameAsync("gohan") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "gohan",
                    Email = "gohan@example.com",
                    Level = Constants.Levels.NotEmployee,
                    Customer = new Customer
                    {
                        CustomerName = "Son Gohan",
                        Gender = true,
                        Address = "Osaka, Japan",
                        Cart = "{\"CartLineItems\":[{\"ProductId\":9,\"Quantity\":1}]}"
                    }
                };

                var result = await userManager.CreateAsync(user, "Cvbnm123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.CustomerRole);
                }
            }

            if (await userManager.FindByNameAsync("chichi") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "chichi",
                    Email = "chichi@example.com",
                    Level = Constants.Levels.NotEmployee,
                    Customer = new Customer
                    {
                        CustomerName = "Chi Chi",
                        Gender = false,
                        Address = "Tokyo, Japan",
                        Cart = "{\"CartLineItems\":[{\"ProductId\":10,\"Quantity\":1}]}"
                    }
                };

                var result = await userManager.CreateAsync(user, "Cvbnm123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.CustomerRole);
                }
            }

            if (await userManager.FindByNameAsync("thor") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "thor",
                    Email = "thor@example.com",
                    Level = Constants.Levels.NotEmployee,
                    Customer = new Customer
                    {
                        CustomerName = "Thor",
                        Gender = true,
                        Address = "New York, US",
                        Cart = string.Empty
                    }
                };

                var result = await userManager.CreateAsync(user, "Cvbnm123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.CustomerRole);
                }
            }
        }

        private static IEnumerable<Order> PopulateSampleOrders()
        {
            return new List<Order>
            {
                new Order {
                    CustomerId = 1,
                    OrderPlaced = new DateTime(2022, 08, 29, 15, 16, 05),
                    OrderTotal = 40.85M,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = 5,
                            Quantity = 1,
                            ProductName = "Prism White T-Shirt",
                            ProductPrice = 18.95M,
                            LineItemTotal = 18.95M,
                        },
                        new OrderDetail
                        {
                            ProductId = 8,
                            Quantity = 2,
                            ProductName = "Cup<T> White Mug",
                            ProductPrice = 10.95M,
                            LineItemTotal = 21.9M,
                        },
                    }
                },
                new Order {
                    CustomerId = 1,
                    OrderPlaced = new DateTime(2022, 08, 29, 15, 18, 20),
                    OrderTotal = 3.95M,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = 12,
                            Quantity = 1,
                            ProductName = "Roslyn Red Badge",
                            ProductPrice = 3.95M,
                            LineItemTotal = 3.95M,
                        },
                    }
                },
                new Order {
                    CustomerId = 2,
                    OrderPlaced = new DateTime(2022, 08, 29, 15, 25, 30),
                    OrderTotal = 15.95M,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = 1,
                            Quantity = 1,
                            ProductName = "Kudu Purple Sweatshirt",
                            ProductPrice = 15.95M,
                            LineItemTotal = 15.95M,
                        },
                    }
                },
            };
        }
    }
}
