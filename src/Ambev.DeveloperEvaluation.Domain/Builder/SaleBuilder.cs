using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Builder;

public class SaleBuilder
{
    private Guid _id;
    private DateTime _createdAt;
    private string _branch = string.Empty;
    private decimal _totalAmount = 0.0m;
    private readonly List<SaleItem> _items = [];

    private readonly SaleCustomerBuilder _saleCustomerBuilder = SaleCustomerBuilder.Empty();

    private SaleBuilder()
    {
    }

    public static SaleBuilder Empty() => new();

    public SaleBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public SaleBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public SaleBuilder WithCustomer(Action<SaleCustomerBuilder> action)
    {
        action(_saleCustomerBuilder);
        return this;
    }

    public SaleBuilder WithBranch(string branch)
    {
        _branch = branch;
        return this;
    }

    public SaleBuilder AddItem(SaleItem item)
    {
        _items.Add(item);
        if (!item.IsCancelled)
        {
            _totalAmount += item.TotalAmount;
        }
        return this;
    }

    public Sale Build()
    {
        return new Sale(_id, _createdAt, _saleCustomerBuilder.Build(), _branch, _totalAmount, _items);
    }
}