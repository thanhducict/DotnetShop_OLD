using BookStore.API.Models;

namespace BookStore.API.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Language Language { get; set; }
        public string ThumbnailUrl { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

        public IEnumerable<AuthorDTO> Authors { get; set; }
    }
}
