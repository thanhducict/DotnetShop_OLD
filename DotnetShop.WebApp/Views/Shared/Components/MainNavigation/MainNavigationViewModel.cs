namespace DotnetShop.WebApp.Views.Shared.Components.MainNavigation
{
    public class MainNavigationViewModel
    {
        public IEnumerable<MainNavigationItemModel> MainNavigationItems { get; set; }
    }

    public class MainNavigationItemModel
    {
        public string AspPage { get; set; }
        public string MenuItemText { get; set; }
        public int Order { get; set; }
    }
}
