using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetShop.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsProductOfTheWeek { get; set; }
        public bool InStock { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
