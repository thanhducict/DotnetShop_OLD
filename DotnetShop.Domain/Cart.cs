namespace DotnetShop.Domain
{
    public class Cart
    {
        public int CartId { get; set; }
        public string CartName { get; set; }
        public DateTime CreatedDate { get; set; }

        // list shopping cart item, change name to CartItem
    }
}
