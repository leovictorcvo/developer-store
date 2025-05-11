using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repository implementation for Sale entity operations
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly IMongoCollection<Sale> _sales;

    public SaleRepository(IMongoDatabase database)
    {
        _sales = database.GetCollection<Sale>("Sales");
        var indexKeys = Builders<Sale>.IndexKeys.Descending(s => s.CreatedAt);
        _sales.Indexes.CreateOne(new CreateIndexModel<Sale>(indexKeys));
    }

    /// <summary>
    /// Adds a new sale to the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _sales.InsertOneAsync(sale, null, cancellationToken);
    }

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Sale> GetByIdAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        Expression<Func<Sale, bool>> filter = applicantRole == UserRole.Customer ?
            s => s.Id == id && s.Customer!.Id == requestedBy :
            s => s.Id == id;

        var sale = await _sales.Find(filter).FirstOrDefaultAsync(cancellationToken) ??
            throw new ResourceNotFoundException("Sale not found", $"Sale with ID '{id}' not found or not belonging to you");
        return sale;
    }

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _sales.ReplaceOneAsync(s => s.Id == sale.Id, sale, new ReplaceOptions { IsUpsert = false }, cancellationToken);
    }

    /// <summary>
    /// Retrieves all sales from the database
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of carts in page</param>
    /// <param name="requestedBy"> The user who requested the cart</param>
    /// <param name="applicantRole"> The role of the user who requested the cart</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Sale>> GetAllAsync(int page, int size, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        Expression<Func<Sale, bool>> filter = applicantRole == UserRole.Customer ?
            s => s.Customer!.Id == requestedBy :
            s => true;

        return await _sales.Find(filter)
            .SortByDescending(s => s.CreatedAt)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a sale by their unique identifier
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id, Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        Expression<Func<Sale, bool>> filter = applicantRole == UserRole.Customer ?
            s => s.Id == id && s.Customer!.Id == requestedBy :
            s => s.Id == id;
        var result = await _sales.DeleteOneAsync(filter, cancellationToken);
        if (result.DeletedCount != 1)
        {
            throw new ResourceNotFoundException("Sale not found", $"Sale with ID '{id}' not found or not belonging to you");
        }
    }

    /// <summary>
    /// Counts the total number of sales in the repository
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<long> CountAsync(Guid requestedBy, UserRole applicantRole, CancellationToken cancellationToken = default)
    {
        Expression<Func<Sale, bool>> filter = applicantRole == UserRole.Customer ?
            s => s.Customer!.Id == requestedBy :
            s => true;
        return await _sales.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
    }
}