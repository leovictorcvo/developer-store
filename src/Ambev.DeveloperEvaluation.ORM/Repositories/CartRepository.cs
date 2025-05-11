using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartRepository using Entity Framework Core
/// </summary>
public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;
    private readonly IConnectionMultiplexer _redis;

    /// <summary>
    /// Initializes a new instance of CartRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CartRepository(DefaultContext context, IConnectionMultiplexer redis)
    {
        _context = context;
        _redis = redis;
    }

    /// <summary>
    /// Creates a new cart in the database
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    public async Task<Cart> CreateAsync(Cart cart, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        if (applicantRole == UserRole.Customer && cart.UserId != requestedBy)
            throw new UnauthorizedAccessException("You can manage only your carts.");

        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

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
    public async Task<Cart> GetByIdAsync(Guid id, bool asNoTracking, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts.AsQueryable();
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        var cart = await query.FirstOrDefaultAsync(o => o.Id == id, cancellationToken) ??
            throw new ResourceNotFoundException("Cart not found", $"Cart with ID '{id}' not found");

        if (applicantRole == UserRole.Customer && cart.UserId != requestedBy)
            throw new UnauthorizedAccessException("You can manage only your carts.");

        return cart;
    }

    /// <summary>
    /// Deletes a cart from the database
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task DeleteAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, asNoTracking: false, requestedBy, applicantRole, cancellationToken);
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Update a cart data in the database
    /// </summary>
    /// <param name="cart">The cart to be updated</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was updated, false if not found</returns>
    public async Task<Cart> UpdateAsync(Cart cart, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        var cartDb = await GetByIdAsync(cart.Id, asNoTracking: false, requestedBy, applicantRole, cancellationToken);
        cartDb.Update(cart.Id, cart.UserId, cart.Date, [.. cart.Products]);
        await _context.SaveChangesAsync(cancellationToken);
        return cartDb;
    }

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
    public async Task<(int, List<Cart>)> GetAllAsync(int page, int size, string? sort, Dictionary<string, string>? filters, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        IDatabase redis = _redis.GetDatabase();

        var query = _context.Carts.AsNoTracking();

        if (filters != null && filters.Count > 0)
        {
            query = await query.ApplyFilters(redis, filters);
        }

        int totalCarts = await query.CountAsync(cancellationToken);

        if (applicantRole == UserRole.Customer)
        {
            query = query.Where(c => c.UserId == requestedBy);
        }

        if ((sort?.Length ?? 0) == 0)
        {
            query = query.OrderByDescending(q => q.Date);
        }
        else
        {
            query = await query.ApplyOrderAsync(redis, sort);
        }

        int skip = (page - 1) * size;
        var carts = await query
            .Skip(skip)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (totalCarts, carts);
    }
}