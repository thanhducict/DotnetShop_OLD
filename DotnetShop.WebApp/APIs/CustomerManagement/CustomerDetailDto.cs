namespace DotnetShop.WebApp.APIs.CustomerManagement
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
