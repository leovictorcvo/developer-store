namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents an address of the user
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// Gets or sets the city of the user
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street of the user
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address number of the user
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zip code of the user
        /// </summary>
        public string ZipCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation of the user
        /// </summary>
        public AddressGeolocation Geolocation { get; set; } = default!;
    }
}