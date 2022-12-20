using BookStore.API.Data;
using BookStore.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.DataLoaders
{
    public class CategoryDataLoader : BatchDataLoader<int, CategoryDTO>
    {
        private readonly BookStoreDbContext _context;

        public CategoryDataLoader(
            IDbContextFactory<BookStoreDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _context = contextFactory.CreateDbContext();
        }

        protected override async Task<IReadOnlyDictionary<int, CategoryDTO>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return await _context.Categories.Where(c => keys.Contains(c.Id)).ToDictionaryAsync(c => c.Id);
        }
    }
}
