namespace DotnetShop.WebApp.APIs.CartManagement
{
    public class CartLineItemDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
