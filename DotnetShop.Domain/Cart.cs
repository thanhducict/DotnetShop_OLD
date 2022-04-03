namespace DotnetShop.Domain
{
    public class Cart
    {
        public List<CartLineItem> CartLineItems { get; set; }
    }

    public class CartLineItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
