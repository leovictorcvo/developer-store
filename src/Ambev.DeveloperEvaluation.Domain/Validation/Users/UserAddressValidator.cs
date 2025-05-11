using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Users;

/// <summary>
/// Validator for user address
/// </summary>
public class UserAddressValidator : AbstractValidator<UserAddress>
{
    /// <summary>
    /// Initializes a new instance of the UserAddressValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Street: Required, length between 1 and 100 characters
    /// - City: Required, length between 1 and 50 characters
    /// - ZipCode: Required, must match the format XXXXX-XXX
    /// - Geolocation: Required, must be a valid geolocation (using AddressGeolocationValidator)
    /// </remarks>
    public UserAddressValidator()
    {
        RuleFor(address => address.Street).NotEmpty().Length(1, 100);
        RuleFor(address => address.City).NotEmpty().Length(1, 50);
        RuleFor(address => address.Number).NotEmpty().WithMessage("Number is required.");
        RuleFor(address => address.ZipCode).NotEmpty().Matches(@"^\d{5}-\d{3}$");
        RuleFor(address => address.Geolocation).NotNull().WithMessage("Geolocation is required.").SetValidator(new AddressGeolocationValidator());
    }
}