namespace DotnetShop.WebApp.Models
{
    public class CartViewModel
    {
        public int CustomerId { get; set; }
        public List<CartLineItemViewModel> CartLineItemViewModels { get; set; }
        public decimal CartTotal { get; set; }
    }

    public class CartLineItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int CartLineItemQuantity { get; set; }
        public decimal CartLineItemTotal { get; set; }
    }
}
