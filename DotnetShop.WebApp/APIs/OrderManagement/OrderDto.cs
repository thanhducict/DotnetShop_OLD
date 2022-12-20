namespace DotnetShop.WebApp.APIs.OrderManagement
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public string CustomerName { get; set; }
    }
}
