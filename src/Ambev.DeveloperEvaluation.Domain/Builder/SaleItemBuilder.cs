using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Builder;

public class SaleItemBuilder
{
    private Guid _id;
    private readonly SaleItemProductBuilder _productBuilder = SaleItemProductBuilder.Empty();
    private bool _isCancelled = false;
    private int _quantity;

    private SaleItemBuilder()
    {
    }

    public static SaleItemBuilder Empty() => new();

    public SaleItemBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public SaleItemBuilder WithProduct(Action<SaleItemProductBuilder> action)
    {
        action(_productBuilder);
        return this;
    }

    public SaleItemBuilder WithIsCancelled(bool isCancelled)
    {
        _isCancelled = isCancelled;
        return this;
    }

    public SaleItemBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public SaleItem Build()
    {
        if (_id == Guid.Empty)
            throw new DomainException("Invalid sales item", "Sale Item ID cannot be empty.");
        if (_quantity < 1 || _quantity > 20)
            throw new DomainException("Invalid sales item", "Quantity must be between 1 and 20.");
        return new SaleItem(_id, _productBuilder.Build(), _quantity, _isCancelled);
    }
}