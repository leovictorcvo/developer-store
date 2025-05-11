using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Extensions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation.Carts;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// The unique identifier of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// The date when the cart was created or updated.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// The list of products in the cart.
    /// </summary>
    public IReadOnlyList<CartItem> Products => _products.AsReadOnly();

    private readonly List<CartItem> _products = [];

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Cart class.
    /// </summary>
    /// <param name="userId">User who owns the cart</param>
    /// <param name="date">when the cart was created</param>
    /// <param name="products">List of products</param>
    public void Create(Guid userId, DateTime date, List<CartItem> products)
    {
        UserId = userId;
        Date = date;
        _products.Clear();
        _products.AddRange(products.GroupItemsByProductId());
        CreatedAt = DateTime.UtcNow;
        Validate();
    }

    /// <summary>
    /// Initializes a new instance of the Cart class.
    /// </summary>
    /// <param name="id">Cart's ID</param>
    /// <param name="userId">User who owns the cart</param>
    /// <param name="date">when the cart was created</param>
    /// <param name="products">List of products</param>
    public void Update(Guid id, Guid userId, DateTime date, List<CartItem> products)
    {
        Id = id;
        UserId = userId;
        Date = date;
        _products.Clear();
        _products.AddRange(products.GroupItemsByProductId());
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    /// <summary>
    /// Performs validation of the cart entity using the CartValidator ruless and throws
    /// a ValidationException if any validation rules are violated.
    /// </summary>
    public void Validate()
    {
        var validator = new CartValidator();
        var validationResult = validator.Validate(this);

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);
    }

    /// <summary>
    /// Validates the references of the cart's user and products.
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="productRepository"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="FluentValidation.ValidationException"></exception>
    public async Task ValidateReferencesAsync(IUserRepository userRepository, IProductRepository productRepository, CancellationToken cancellationToken = default)
    {
        await userRepository.IsUserRegisteredAndActive(UserId, cancellationToken);
        var productIds = Products.Select(x => x.ProductId).Distinct();
        var invalidProductIds = await productRepository.AllIdsAreValidsAsync(productIds, cancellationToken);
        if (invalidProductIds.Count > 0)
            throw new ResourceNotFoundException("Product not found", $"The following product IDs were not found: {string.Join(',', invalidProductIds)}");
    }
}