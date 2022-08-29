namespace DotnetShop.WebApp.APIs.OrderManagement
{
    public class OrderWithDetailDto
    {
        public int OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public List<OrderDetailDto> OrderDetailViewModels { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
