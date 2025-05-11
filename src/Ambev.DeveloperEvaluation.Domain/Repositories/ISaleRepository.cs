using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Adds a new sale to the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Sale> GetByIdAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales from the database
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of carts in page</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Sale>> GetAllAsync(int page, int size, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale by their unique identifier
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the total number of sales in the repository
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<long> CountAsync(Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default);
}