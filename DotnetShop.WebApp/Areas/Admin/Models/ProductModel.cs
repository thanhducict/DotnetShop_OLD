using System.ComponentModel.DataAnnotations;

namespace DotnetShop.WebApp.Areas.Admin.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }

        [Display(Name = "Image Thumbnail Url")]
        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Image Thumbnail")]
        public IFormFile ImageThumbnail { get; set; }

        [Display(Name = "Is Product Of The Week")]
        public bool IsProductOfTheWeek { get; set; }

        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        public int CategoryId { get; set; }
        public CategoryDropdown CategoryDropdown { get; set; }
    }

    public class CategoryDropdown
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
