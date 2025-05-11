using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;

/// <summary>
/// Represents a response containing user information.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    ///The user's name
    /// </summary>
    public UserName Name { get; set; } = default!;

    /// <summary>
    /// The user's address
    /// </summary>
    public UserAddress Address { get; set; } = default!;

    /// <summary>
    /// The user's phone number in international format (+XXXXXXXXXXX)
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The user's status
    /// </summary>
    public string Status { get; set; } = string.Empty;
}