namespace DotnetShop.Admin.OrderManagement.Models
{
    public class OrderDetailViewModel
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineItemTotal { get; set; }
    }
}
