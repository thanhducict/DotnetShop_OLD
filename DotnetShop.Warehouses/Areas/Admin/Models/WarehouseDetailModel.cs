namespace DotnetShop.Warehouses.Areas.Admin.Models
{
    public class WarehouseDetailModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<ProductModel> ProductModels { get; set; }
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
    }
}
