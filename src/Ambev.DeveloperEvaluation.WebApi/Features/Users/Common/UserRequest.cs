using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;

public abstract class UserRequest
{
    /// <summary>
    /// Gets or sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username. Must be unique and contain only valid characters.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password. Must meet security requirements.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or set the name of the user.
    /// </summary>
    public UserName Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address of the user.
    /// </summary>
    public UserAddress Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the phone number in international format (+XXXXXXXXXXX)
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial status of the user account.
    /// </summary>
    /// <example>
    /// Active, Inactive, Suspended
    /// </example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    /// <example>
    /// Customer, Manager, Admin
    /// </example>
    public string Role { get; set; } = string.Empty;
}