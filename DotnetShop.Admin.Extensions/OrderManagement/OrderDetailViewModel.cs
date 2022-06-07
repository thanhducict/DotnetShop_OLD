namespace DotnetShop.Admin.Extensions.OrderManagement
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineItemTotal { get; set; }
    }
}
