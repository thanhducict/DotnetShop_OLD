namespace DotnetShop.Warehouses.Areas.Admin.Models
{
    public class WarehouseLocationModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public List<WarehouseModel> WarehouseModels { get; set; }
    }


}
