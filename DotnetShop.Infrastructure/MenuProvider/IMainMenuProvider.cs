namespace DotnetShop.Infrastructure.MenuProvider
{
    /// <summary>
    /// The menu provider for main Website Navigation.
    /// </summary>
    public interface IMainMenuProvider
    {
        /// <summary>
        /// Provides the menu items.
        /// </summary>
        /// <returns>
        /// A list of <see cref="MainMenuItem"/>s that the provider exposes.
        /// </returns>
        public IEnumerable<MainMenuItem> GetMenuItems();
    }
}
