using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command to update an existing user.
/// </summary>
public class UpdateUserCommand : ApplicationUserCommand
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to be updated.
    /// </summary>
    /// <value>A GUID that uniquely identifies a existing user in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user who requested the cart creation.
    /// </summary>
    public Guid RequestedBy { get; set; }

    /// <summary>
    /// The role of the user who requested the cart creation.
    /// </summary>
    public UserRole ApplicantRole { get; set; }
}