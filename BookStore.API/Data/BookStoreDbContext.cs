using BookStore.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        public DbSet<BookDTO> Books { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<AuthorDTO> Authors { get; set; }
    }
}
