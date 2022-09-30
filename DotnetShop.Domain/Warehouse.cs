namespace DotnetShop.Domain
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Address { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
