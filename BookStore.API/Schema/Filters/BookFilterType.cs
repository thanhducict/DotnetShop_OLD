using BookStore.API.Schema.Queries;
using HotChocolate.Data.Filters;

namespace BookStore.API.Schema.Filters
{
    public class BookFilterType : FilterInputType<BookType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<BookType> descriptor)
        {
            descriptor.Ignore(c => c.Authors);

            base.Configure(descriptor);
        }
    }
}
