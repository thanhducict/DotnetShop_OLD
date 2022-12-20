using BookStore.API.Models;

namespace BookStore.API.Schema.Mutations
{
    public class BookResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Language Language { get; set; }
        public string Thumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
