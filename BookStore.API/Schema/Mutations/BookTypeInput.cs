using BookStore.API.Models;

namespace BookStore.API.Schema.Mutations
{
    public class BookTypeInput
    {
        public string Title { get; set; }
        public Language Language { get; set; }
        public string Thumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
