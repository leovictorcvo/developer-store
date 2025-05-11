namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents the geolocation of a user.
    /// </summary>
    public class AddressGeolocation
    {
        /// <summary>
        /// Gets or sets the latitude of the user.
        /// </summary>
        public string Lat { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the longitude of the user.
        /// </summary>
        public string Long { get; set; } = string.Empty;
    }
}