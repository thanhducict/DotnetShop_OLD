namespace DotnetShop.Admin.Extensions.OrderManagement
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public List<OrderDetailViewModel> OrderDetailViewModels { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
