using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Builder;

public class SaleCustomerBuilder
{
    private Guid _id;
    private string name = string.Empty;

    private SaleCustomerBuilder()
    {
    }

    public static SaleCustomerBuilder Empty() => new();

    public SaleCustomerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public SaleCustomerBuilder WithName(string name)
    {
        this.name = name;
        return this;
    }

    public SaleCustomer Build()
    {
        if (_id == Guid.Empty)
            throw new DomainException("Invalid sales customer", "Customer ID cannot be empty.");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Invalid sales customer", "Customer name cannot be empty.");

        return new SaleCustomer(_id, name);
    }
}