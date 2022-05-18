using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetShop.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Image Thumbnail Url")]
        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Is Product Of The Week")]
        public bool IsProductOfTheWeek { get; set; }

        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
