namespace DotnetShop.Data.Domain
{
    public class ProductWarehouse
    {
        public int ProductWarehouseId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
