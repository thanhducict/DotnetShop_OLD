using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetShop.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal OrderTotal { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
