namespace BookStore.API.Schema.Queries
{
    public class AuthorType : ISearchResultType
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
    }
}
