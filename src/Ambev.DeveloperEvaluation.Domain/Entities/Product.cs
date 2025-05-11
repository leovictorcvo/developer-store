using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product's title.
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's price.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Gets or sets the product's description.
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's category.
    /// </summary>
    public string Category { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's image URL.
    /// </summary>
    public string Image { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's rating.
    /// </summary>
    public Rating Rating { get; private set; } = default!;

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public void Create(string title, decimal price, string description, string category, string image, Rating rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category.ToUpper();
        Image = image;
        Rating = rating;
        CreatedAt = DateTime.UtcNow;
        Validate();
    }

    /// <summary>
    /// Updates the product's information.
    /// </summary>
    /// <param name="id">Product's ID</param>
    /// <param name="title">Product's title</param>
    /// <param name="price">Product's price</param>
    /// <param name="description">Product's description</param>
    /// <param name="category">Product's category</param>
    /// <param name="image">Product's image</param>
    /// <param name="rating">Product's rating</param>
    public void Update(Guid id, string title, decimal price, string description, string category, string image, Rating rating)
    {
        Id = id;
        Title = title;
        Price = price;
        Description = description;
        Category = category.ToUpper();
        Image = image;
        Rating = rating;
        UpdatedAt = DateTime.UtcNow;
        Validate(isUpdate: true);
    }

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator ruless and throws
    /// a ValidationException if any validation rules are violated.
    /// </summary>
    public void Validate(bool isUpdate = false)
    {
        var validator = new ProductValidator();
        var validationResult = validator.Validate(this);

        if (isUpdate && this.Id == Guid.Empty)
        {
            validationResult.Errors.Add(
                new FluentValidation.Results.ValidationFailure(nameof(Id), "Product ID is required."));
        }

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);
    }
}