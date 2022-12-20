namespace DotnetShop.WebApp.APIs.CustomerManagement
{
    /// <summary>
    /// The class to transfer customer data.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// The customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

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
    }
}
