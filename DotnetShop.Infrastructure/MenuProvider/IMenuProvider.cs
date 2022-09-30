namespace DotnetShop.Infrastructure.MenuProvider
{
    /// <summary>
    /// The menu provider.
    /// </summary>
    public interface IMenuProvider
    {
        /// <summary>
        /// Provides the menu items of the Admin navigation.
        /// </summary>
        /// <returns>
        /// A list of <see cref="MenuItem"/>s that the provider exposes.
        /// </returns>
        public IEnumerable<MenuItem> GetMenuItems();
    }
}
