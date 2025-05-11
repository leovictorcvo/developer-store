using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new product in the repository
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="asNoTracking">Whether to use AsNoTracking</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found otherwise throws ResourceNotFoundException</returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    Task<Product> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by their title
    /// </summary>
    /// <param name="title">The title to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found otherwise null</returns>
    Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a product data in the database
    /// </summary>
    /// <param name="product">The product to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was updated, false if not found</returns>
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all products with pagination and sorting options
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of products in page</param>
    /// <param name="sort">Sort directions. Optional</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<(int, List<Product>)> GetAllAsync(int page, int size, string? sort, Dictionary<string, string>? filters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all products by catory name with pagination and sorting options
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of products in page</param>
    /// <param name="sort">Sort directions. Optional</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<(int, List<Product>)> GetByCategoryAsync(string category, int page, int size, string? sort, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if all provided IDs are valid and exist in the repository
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>List of invalid ids</returns>
    Task<List<Guid>> AllIdsAreValidsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}