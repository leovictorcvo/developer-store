using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the repository
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their unique identifier or throws ResourceNotFoundException if not found
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="asNoTracking">Whether to use AsNoTracking</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found otherwise throws ResourceNotFoundException</returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    Task<User> GetByIdAsync(Guid id, bool asNoTracking, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ResourceNotFoundException"></exception>
    Task DeleteAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a user data in the database
    /// </summary>
    /// <param name="user">The user to be updated</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated user</returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    Task<User> UpdateAsync(User user, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all users with pagination and sorting options
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of users in page</param>
    /// <param name="sort">Sort directions. Optional</param>
    /// <param name="filters">Filters to apply. Optional</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<(int, List<User>)> GetAllAsync(int page, int size, string? sort, Dictionary<string, string>? filters, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user is registered and active in the system
    /// </summary>
    /// <param name="id">User id to check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task IsUserRegisteredAndActive(Guid id, CancellationToken cancellationToken = default);
}