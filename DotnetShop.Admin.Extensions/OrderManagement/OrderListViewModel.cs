namespace DotnetShop.Admin.Extensions.OrderManagement
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public string CustomerName { get; set; }
    }
}
