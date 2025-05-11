using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Builder;

public class SaleItemProductBuilder
{
    private Guid _id;
    private string _name = string.Empty;
    private decimal _unitPrice;

    private SaleItemProductBuilder()
    { }

    public static SaleItemProductBuilder Empty() => new();

    public SaleItemProductBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public SaleItemProductBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SaleItemProductBuilder WithUnitPrice(decimal unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public SaleItemProduct Build()
    {
        if (_id == Guid.Empty)
            throw new DomainException("Invalid product", "Product ID cannot be empty.");
        if (string.IsNullOrEmpty(_name))
            throw new DomainException("Invalid product", "Product name cannot be empty.");
        if (_unitPrice <= 0)
            throw new DomainException("Invalid product", "Product unit price must be greater than zero.");
        return new SaleItemProduct(_id, _name, _unitPrice);
    }
}