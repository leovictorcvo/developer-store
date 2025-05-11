using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;
    private readonly IConnectionMultiplexer _redis;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context, IConnectionMultiplexer redis)
    {
        _context = context;
        _redis = redis;
    }

    /// <summary>
    /// Creates a new product in the database
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="asNoTracking">Whether to use AsNoTracking</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found otherwise throws ResourceNotFoundException</returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<Product> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(o => o.Id == id, cancellationToken) ??
            throw new ResourceNotFoundException("Product not found", $"Product with ID '{id}' not found");
    }

    /// <summary>
    /// Retrieves a product by their title
    /// </summary>
    /// <param name="title">The title to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found otherwise null</returns>
    public async Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Title == title, cancellationToken);
    }

    /// <summary>
    /// Deletes a product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, asNoTracking: false, cancellationToken);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Update a product data in the database
    /// </summary>
    /// <param name="product">The product to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was updated, false if not found</returns>
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var productDb = await GetByIdAsync(product.Id, asNoTracking: false, cancellationToken);
        productDb.Update(product.Id, product.Title, product.Price, product.Description, product.Category, product.Image, product.Rating);
        await _context.SaveChangesAsync(cancellationToken);
        return productDb;
    }

    /// <summary>
    /// Retrieves all products with pagination and sorting options
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of products in page</param>
    /// <param name="sort">Sort directions. Optional</param>
    /// <param name="filters">Filters to apply. Optional</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<(int, List<Product>)> GetAllAsync(int page, int size, string? sort, Dictionary<string, string>? filters, CancellationToken cancellationToken = default)
    {
        IDatabase redis = _redis.GetDatabase();

        var query = _context.Products.AsNoTracking();

        if (filters != null && filters.Count > 0)
        {
            query = await query.ApplyFilters(redis, filters);
        }

        int totalProducts = await query.CountAsync(cancellationToken);

        if ((sort?.Length ?? 0) == 0)
        {
            query = query.OrderBy(q => q.Title);
        }
        else
        {
            query = await query.ApplyOrderAsync(_redis.GetDatabase(), sort);
        }

        int skip = (page - 1) * size;
        var products = await query
            .Skip(skip)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (totalProducts, products);
    }

    /// <summary>
    /// Retrieves all product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        => _context.Products
            .AsNoTracking()
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync(cancellationToken);

    public async Task<(int, List<Product>)> GetByCategoryAsync(string category, int page, int size, string? sort, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsNoTracking().Where(p => p.Category == category.ToUpper());

        int totalProducts = await query.CountAsync(cancellationToken);

        if ((sort?.Length ?? 0) == 0)
        {
            query = query.OrderBy(q => q.Title);
        }
        else
        {
            query = await query.ApplyOrderAsync(_redis.GetDatabase(), sort);
        }

        int skip = (page - 1) * size;
        var products = await query
            .Skip(skip)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (totalProducts, products);
    }

    public async Task<List<Guid>> AllIdsAreValidsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var existingIds = await _context.Products
            .Where(p => ids!.Contains(p.Id))
            .Select(p => p.Id)
            .ToListAsync(cancellationToken);

        return [.. ids!.Except(existingIds)];
    }
}