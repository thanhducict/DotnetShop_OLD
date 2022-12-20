using BookStore.API.DTOs;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreSeeding
    {
        public static async Task SeedAsync(BookStoreDbContext context, ILogger logger)
        {
            await context.Database.EnsureCreatedAsync();

            var categories = PopulateSampleCategories();
            var authors = PopulateSampleAuthors();

            try
            {
                if (!await context.Categories.AnyAsync())
                {
                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                }
                if (!await context.Books.AnyAsync())
                {
                    await context.Books.AddRangeAsync(PopulateSampleBooks(authors.ToList()));
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        private static IEnumerable<CategoryDTO> PopulateSampleCategories()
        {
            return new List<CategoryDTO> {
                new CategoryDTO { Id = 1, Name = "Programming", CODE = "CAT_PROGRAMING" },
                new CategoryDTO { Id = 2, Name = "Servers", CODE = "CAT_SERVERS" },
                new CategoryDTO { Id = 3, Name = "Certifications", CODE = "CAT_CERTIFICATIONS" }
            };
        }

        private static IEnumerable<AuthorDTO> PopulateSampleAuthors()
        {
            return new List<AuthorDTO> {
                new AuthorDTO { Id = 1, FirstName = "John", LastName = "Sharp", Salary = 1500 },
                new AuthorDTO { Id = 2, FirstName = "Dino", LastName = "Esposito", Salary = 2000 },
                new AuthorDTO { Id = 3, FirstName = "Francesco", LastName = "Esposito", Salary = 2200 },
                new AuthorDTO { Id = 4, FirstName = "Vittorio", LastName = "Bertocci", Salary = 1600 },
                new AuthorDTO { Id = 5, FirstName = "Kalen", LastName = "Delaney", Salary = 1800 },
                new AuthorDTO { Id = 6, FirstName = "Craig", LastName = "Freeman", Salary = 1600 },
                new AuthorDTO { Id = 7, FirstName = "Andrew", LastName = "Bettany", Salary = 900 },
                new AuthorDTO { Id = 8, FirstName = "Andrew", LastName = "Warren", Salary = 850 },
                new AuthorDTO { Id = 9, FirstName = "Joan", LastName = "Lambert", Salary = 700 },
            };
        }

        private static IEnumerable<BookDTO> PopulateSampleBooks(List<AuthorDTO> authors)
        {
            return new List<BookDTO> {
                new BookDTO
                {
                    Id = 1,
                    Title = "C# Step by Step",
                    Language = Language.English,
                    ThumbnailUrl = "/assets/images/CSharp.jpg",
                    CategoryId = 1,
                    Authors = new List<AuthorDTO>
                    {
                        authors[0],
                    }
                },
                new BookDTO
                {
                    Id = 2,
                    Title = "Programming ML.NET",
                    Language = Language.English,
                    CategoryId = 1,
                    ThumbnailUrl = "/assets/images/ML.jpg",
                    Authors = new List<AuthorDTO>
                    {
                        authors[1],
                        authors[2],
                    }
                },
                new BookDTO
                {
                    Id = 3,
                    Title = "Modern Authentication with Azure Active Directory for Web Applications",
                    Language = Language.English,
                    CategoryId = 2,
                    ThumbnailUrl = "/assets/images/ModernAuthen.jpg",
                    Authors = new List<AuthorDTO>
                    {
                        authors[3],
                    }
                },
                new BookDTO
                {
                    Id = 4,
                    Title = "Microsoft SQL Server 2012 Internals",
                    Language = Language.Vietnamese,
                    CategoryId = 2,
                    ThumbnailUrl = "/assets/images/SQL2012.jpg",
                    Authors = new List<AuthorDTO>
                    {
                        authors[4],
                        authors[5],
                    }
                },
                new BookDTO
                {
                    Id = 5,
                    Title = "Exam Ref MD-101 Managing Modern Desktops, 2nd Edition",
                    Language = Language.English,
                    CategoryId = 3,
                    ThumbnailUrl = "/assets/images/Exam.jpg",
                    Authors = new List<AuthorDTO>
                    {
                        authors[6],
                        authors[7],
                    }
                },
                new BookDTO
                {
                    Id = 6,
                    Title = "MOS Study Guide for Microsoft Outlook Exam MO-400",
                    Language = Language.Mandarin,
                    CategoryId = 3,
                    ThumbnailUrl = "/assets/images/MOS.jpg",
                    Authors = new List<AuthorDTO>
                    {
                        authors[8],
                    }
                },
            };
        }
    }
}
