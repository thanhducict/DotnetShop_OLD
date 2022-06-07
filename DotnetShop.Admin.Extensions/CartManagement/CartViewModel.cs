namespace DotnetShop.Admin.Extensions.CartManagement
{
    public class CartManagementViewModel
    {
        public List<CartLineItemViewModel> AllCartLineItems { get; set; }
    }

    public class CartLineItemViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
