using DotnetShop.Admin.Warehouses.Attributes;

namespace DotnetShop.Admin.Warehouses.Areas.Admin.Models
{
    [WarehouseUtil(DefaultName = "[Name is missing]")]
    public class WarehouseModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
