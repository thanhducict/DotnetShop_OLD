namespace DotnetShop.Admin.OrderManagement.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
