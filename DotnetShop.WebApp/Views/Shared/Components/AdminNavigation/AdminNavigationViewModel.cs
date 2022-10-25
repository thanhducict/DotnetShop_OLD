namespace DotnetShop.WebApp.Views.Shared.Components.AdminNavigation
{
    public class AdminNavigationViewModel
    {
        public IEnumerable<AdminNavigationItemModel> AdminNavigationItems { get; set; }
    }

    public class AdminNavigationItemModel
    {
        public string AspArea { get; set; }
        public string AspController { get; set; }
        public string AspAction { get; set; }
        public string AspPage { get; set; }
        public string MenuItemText { get; set; }
        public int Order { get; set; }
    }
}
