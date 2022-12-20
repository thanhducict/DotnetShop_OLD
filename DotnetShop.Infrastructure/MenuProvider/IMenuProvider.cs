namespace DotnetShop.Infrastructure.MenuProvider
{
    public interface IMenuProvider<T>
    {
        /// <summary>
        /// Provides the menu items of the navigation.
        /// </summary>
        /// <returns>
        /// A list of <see cref="T"/>s that the provider exposes.
        /// </returns>
        public IEnumerable<T> GetMenuItems();
    }
}
