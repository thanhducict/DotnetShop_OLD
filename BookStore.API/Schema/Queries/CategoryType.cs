namespace BookStore.API.Schema.Queries
{
    public class CategoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [GraphQLName("code")]
        public string CODE { get; set; }
    }
}
