using BookStore.API.Schema.Queries;
using HotChocolate.Data.Sorting;

namespace BookStore.API.Schema.Sorters
{
    public class BookSortType : SortInputType<BookType>
    {
        protected override void Configure(ISortInputTypeDescriptor<BookType> descriptor)
        {
            descriptor.Ignore(c => c.Id);
            descriptor.Ignore(c => c.Authors);
            descriptor.Ignore(c => c.ThumbnailUrl);

            base.Configure(descriptor);
        }
    }
}
