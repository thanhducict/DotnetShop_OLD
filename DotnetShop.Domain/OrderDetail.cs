using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetShop.Domain
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal LineItemTotal { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
