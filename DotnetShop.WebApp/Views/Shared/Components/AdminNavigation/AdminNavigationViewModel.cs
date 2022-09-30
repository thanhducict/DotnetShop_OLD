namespace DotnetShop.WebApp.Views.Shared.Components.AdminNavigation
{
    public class AdminNavigationViewModel
    {
        public IEnumerable<NavigationItemModel> NavigationItems { get; set; }
    }

    public class NavigationItemModel
    {
        public string AspArea { get; set; }
        public string AspController { get; set; }
        public string AspAction { get; set; }
        public string AspPage { get; set; }
        public string MenuText { get; set; }
    }
}
