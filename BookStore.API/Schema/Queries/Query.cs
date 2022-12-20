using BookStore.API.Data;
using BookStore.API.Schema.Filters;
using BookStore.API.Schema.Sorters;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Schema.Queries
{
    public class Query
    {
        private readonly BookStoreDbContext _context;

        public Query(IDbContextFactory<BookStoreDbContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public IQueryable<BookType> GetAllBooksInfo()
        {
            return _context.Books.Select(b => new BookType
            {
                Id = b.Id,
                Title = b.Title,
                Language = b.Language,
                ThumbnailUrl = b.ThumbnailUrl,
                CategoryId = b.CategoryId,
                Authors = b.Authors.Select(a => new AuthorType 
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Salary = a.Salary,
                })
            });
        }

        [UseDbContext(typeof(BookStoreDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 6)]
        [UseProjection]
        [UseFiltering(typeof(BookFilterType))]
        [UseSorting(typeof(BookSortType))]
        public IQueryable<BookType> GetBooks([ScopedService] BookStoreDbContext paramContext)
        {
            return paramContext.Books.Select(b => new BookType
            {
                Id = b.Id,
                Title = b.Title,
                Language = b.Language,
                ThumbnailUrl = b.ThumbnailUrl,
                CategoryId = b.CategoryId,
                Authors = b.Authors.Select(a => new AuthorType
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Salary = a.Salary,
                })
            });
        }

        public async Task<BookType?> GetBookByIdAsync(int id)
        {
            return await _context.Books.Select(b => new BookType
            {
                Id = b.Id,
                Title = b.Title,
                Language = b.Language,
                ThumbnailUrl = b.ThumbnailUrl,
                CategoryId = b.CategoryId,
            }).Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ISearchResultType>> Search(string term)
        {
            IEnumerable<BookType> courses = await _context.Books
                .Where(b => b.Title.Contains(term))
                .Select(b => new BookType()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Language = b.Language,
                    CategoryId = b.CategoryId,
                })
                .ToListAsync();

            IEnumerable<AuthorType> authors = await _context.Authors
                .Where(i => i.FirstName.Contains(term) || i.LastName.Contains(term))
                .Select(i => new AuthorType()
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Salary = i.Salary,
                })
                .ToListAsync();

            return new List<ISearchResultType>().Concat(courses).Concat(authors);
        }

        [GraphQLDeprecated("This query is depricated.")]
        public string Instructions => "Hello World";
    }
}
