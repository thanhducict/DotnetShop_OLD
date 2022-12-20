using BookStore.API.DataLoaders;
using BookStore.API.Models;

namespace BookStore.API.Schema.Queries
{
    public class BookType : ISearchResultType
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Language Language { get; set; }
        public string? ThumbnailUrl { get; set; }

        [IsProjected(true)]
        public int CategoryId { get; set; }

        [GraphQLNonNullType]
        public async Task<CategoryType> Category([Service] CategoryDataLoader categoryDataLoader)
        {
            var result = await categoryDataLoader.LoadAsync(CategoryId, CancellationToken.None);
            if (result != null)
            {
                return new CategoryType
                {
                    Id = result.Id,
                    Name = result.Name,
                    CODE = result.CODE
                };
            }
            else
            {
                return new CategoryType();
            }
        }
        public IEnumerable<AuthorType>? Authors { get; set; }
    }
}
