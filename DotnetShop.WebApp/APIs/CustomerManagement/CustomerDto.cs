namespace DotnetShop.WebApp.APIs.CustomerManagement
{
    /// <summary>
    /// The class to transfer customer data.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// The name of customer.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// The gender of customer.
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// The address of customer.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The country of customer.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The cart of customer.
        /// </summary>
        public string Cart { get; set; }
    }
}
