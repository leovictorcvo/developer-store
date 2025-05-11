using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;
    private readonly IConnectionMultiplexer _redis;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context, IConnectionMultiplexer redis)
    {
        _context = context;
        _redis = redis;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

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
    public async Task<User> GetByIdAsync(Guid id, bool asNoTracking, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        if (applicantRole == UserRole.Customer && id != requestedBy)
            throw new ForbiddenAccessException("You can manage only your data.");

        var query = _context.Users.AsQueryable();
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(o => o.Id == id, cancellationToken) ??
            throw new ResourceNotFoundException("User not found", $"User with ID '{id}' not found");
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task DeleteAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, asNoTracking: false, requestedBy, applicantRole, cancellationToken);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Update a user data in the database
    /// </summary>
    /// <param name="user">The user to be updated</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated user</returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<User> UpdateAsync(User user, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        var userDb = await GetByIdAsync(user.Id, asNoTracking: false, requestedBy, applicantRole, cancellationToken);
        userDb.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return userDb;
    }

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
    public async Task<(int, List<User>)> GetAllAsync(int page, int size, string? sort, Dictionary<string, string>? filters, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        IDatabase redis = _redis.GetDatabase();

        var query = _context.Users.AsNoTracking();

        if (filters != null && filters.Count > 0)
        {
            query = await query.ApplyFilters(redis, filters);
        }

        int totalUsers = await query.CountAsync(cancellationToken);

        if (applicantRole == UserRole.Customer)
        {
            query = query.Where(u => u.Id == requestedBy);
        }

        if ((sort?.Length ?? 0) == 0)
        {
            query = query.OrderBy(q => q.Username);
        }
        else
        {
            query = await query.ApplyOrderAsync(redis, sort);
        }

        int skip = (page - 1) * size;
        var users = await query
            .Skip(skip)
            .Take(size)
            .ToListAsync(cancellationToken);

        return (totalUsers, users);
    }

    /// <summary>
    /// Checks if a user is registered and active in the system
    /// </summary>
    /// <param name="id">User id to check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task IsUserRegisteredAndActive(Guid id, CancellationToken cancellationToken = default)
    {
        _ = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && u.Status == UserStatus.Active, cancellationToken) ??
            throw new ResourceNotFoundException("User not found", $"User with ID '{id}' not found");
    }
}