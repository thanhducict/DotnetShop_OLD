namespace DotnetShop.Infrastructure.MenuProvider
{
    /// <summary>
    /// The menu provider for Admin Navigation.
    /// </summary>
    public interface IAdminMenuProvider
    {
        /// <summary>
        /// Provides the menu items of the Admin navigation.
        /// </summary>
        /// <returns>
        /// A list of <see cref="AdminMenuItem"/>s that the provider exposes.
        /// </returns>
        public IEnumerable<AdminMenuItem> GetMenuItems();
    }
}
