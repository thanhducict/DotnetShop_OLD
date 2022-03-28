using DotnetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Data
{
    public class DotnetShopDbContext : DbContext
    {
        public DotnetShopDbContext(DbContextOptions<DotnetShopDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Shirt" });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Kudu Purple Sweatshirt",
                Price = 15.95M,
                ShortDescription = "Our famous Kudu Purple Sweatshirt!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "Kudu Purple Sweatshirt HD.png",
                InStock = true,
                IsProductOfTheWeek = true,
                ImageThumbnailUrl = "Kudu Purple Sweatshirt"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = ".NET Blue Sweatshirt",
                Price = 18.95M,
                ShortDescription = "You'll love it!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "NET Blue Sweatshirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "NET Blue Sweatshirt.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = ".NET Bot Black Sweatshirt",
                Price = 18.95M,
                ShortDescription = "Best seller in year.",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "NET Bot Black Sweatshirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "NET Bot Black Sweatshirt.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = ".NET Foundation Sweatshirt",
                Price = 17.95M,
                ShortDescription = "A summer classic!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "NET Foundation Sweatshirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "NET Foundation Sweatshirt.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Prism White T-Shirt",
                Price = 18.95M,
                ShortDescription = "Welcome summer with this shirt!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "Prism White T-Shirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "Prism White T-Shirt.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "Roslyn Red T-Shirt",
                Price = 17.95M,
                ShortDescription = "A summer favorite",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "Roslyn Red T-Shirt HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "Roslyn Red T-Shirt.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 7,
                Name = "Cup<T> T-Shirt",
                Price = 19.95M,
                ShortDescription = "Simple the best!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 1,
                ImageUrl = "CupT T-Shirt HD.png",
                InStock = false,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "CupT T-Shirt.png"
            });

            // ========================= mot nua seed vao thoi diem runtime

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Cup" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Badge" });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 8,
                Name = "Cup<T> White Mug",
                Price = 10.95M,
                ShortDescription = "a new designed cup.",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 2,
                ImageUrl = "CupT White Mug HD.png",
                InStock = true,
                IsProductOfTheWeek = true,
                ImageThumbnailUrl = "CupT White Mug.png"
            });


            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 9,
                Name = ".NET Black White Mug",
                Price = 10.95M,
                ShortDescription = "A classic design cup.",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 2,
                ImageUrl = "NET Black White Mug HD.png",
                InStock = true,
                IsProductOfTheWeek = true,
                ImageThumbnailUrl = "NET Black White Mug.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 10,
                Name = "Cup<T> Badge",
                Price = 4.95M,
                ShortDescription = "A geeky badge!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 3,
                ImageUrl = "CupT Badge HD.png",
                InStock = true,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "CupT Badge.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 11,
                Name = "NET Foundation Badge",
                Price = 3.95M,
                ShortDescription = "You'll love it!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 3,
                ImageUrl = "NET Foundation Badge HD.png",
                InStock = false,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "NET Foundation Badge.png"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 12,
                Name = "Roslyn Red Badge",
                Price = 3.95M,
                ShortDescription = "You'll love it!",
                LongDescription =
                    "Let's unpack that later product launch. Cloud strategy throughput but put a record on and see who dances we need to leverage our synergies. Are there any leftovers in the kitchen? downselect streamline, so we don't want to boil the ocean. Product market fit make it a priority today shall be a cloudy day, thanks to blue sky thinking, we can now deploy our new ui to the cloud or conversational content for we need to make the new version clean and sexy, moving the goalposts get six alpha pups in here for a focus group. Low hanging fruit how much bandwidth do you have rock Star/Ninja we don't need to boil the ocean here big picture back to the drawing-board. Build on a culture of contribution and inclusion a set of certitudes based on deductions founded on false premise killing it, for screw the pooch, so closing these latest prospects is like putting socks on an octopus, and show pony. Quick win i am dead inside, this vendor is incompetent or we don't want to boil the ocean or looks great, can we try it a different way, or goalposts for move the needle. Digital literacy we need to touch base off-line before we fire the new ux experience but hit the ground running. No scraps hit the floor red flag, so synergestic actionables ultimate measure of success thinking outside the box, and viral engagement, nor I just wanted to give you a heads-up.",
                CategoryId = 3,
                ImageUrl = "Roslyn Red Badge HD.png",
                InStock = false,
                IsProductOfTheWeek = false,
                ImageThumbnailUrl = "Roslyn Red Badge.png"
            });
        }

    }
}

