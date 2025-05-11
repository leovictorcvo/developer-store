using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Cart entity operations
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Creates a new cart in the database
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    Task<Cart> CreateAsync(Cart cart, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="asNoTracking">Whether to use AsNoTracking</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found otherwise throws ResourceNotFoundException</returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    Task<Cart> GetByIdAsync(Guid id, bool asNoTracking, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a cart from the database
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a cart data in the database
    /// </summary>
    /// <param name="cart">The cart to be updated</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was updated, false if not found</returns>
    Task<Cart> UpdateAsync(Cart cart, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all carts with pagination and sorting options
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of carts in page</param>
    /// <param name="sort">Sort directions. Optional</param>
    /// <param name="filters">Filters to apply. Optional</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<(int, List<Cart>)> GetAllAsync(int page, int size, string? sort, Dictionary<string, string>? filters, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);
}