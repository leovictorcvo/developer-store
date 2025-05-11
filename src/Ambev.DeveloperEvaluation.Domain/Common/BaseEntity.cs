﻿using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

[ExcludeFromCodeCoverage]
public abstract class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }
}