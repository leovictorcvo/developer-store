using Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Represents a request to update a existing user in the system.
/// </summary>
public class UpdateUserRequest : UserRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to be updated.
    /// </summary>
    /// <value>A GUID that uniquely identifies a existing user in the system.</value>
    public Guid Id { get; set; }
}