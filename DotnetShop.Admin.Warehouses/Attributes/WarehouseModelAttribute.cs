namespace DotnetShop.Admin.Warehouses.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class WarehouseUtilAttribute : Attribute
    {
        public string DefaultName { get; set; }
    }
}
