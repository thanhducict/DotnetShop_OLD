namespace DotnetShop.WebApp.APIs.OrderManagement
{
    public class OrderCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
    }
}
