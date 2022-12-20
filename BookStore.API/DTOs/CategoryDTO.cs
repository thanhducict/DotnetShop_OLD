namespace BookStore.API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [GraphQLName("code")]
        public string CODE { get; set; }

        public IEnumerable<BookDTO> Books { get; set; }
    }
}
