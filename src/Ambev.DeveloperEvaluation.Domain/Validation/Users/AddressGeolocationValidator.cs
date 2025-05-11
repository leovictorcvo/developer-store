using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Users;

/// <summary>
/// Validator for address geolocation
/// </summary>
public class AddressGeolocationValidator : AbstractValidator<AddressGeolocation>
{
    /// <summary>
    /// Initializes a new instance of the AddressGeolocationValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Lat: Required and valid number
    /// - Long: Required and valid number
    /// </remarks>
    public AddressGeolocationValidator()
    {
        RuleFor(geolocation => geolocation.Lat)
            .NotEmpty()
            .WithMessage("Latitude is required.")
            .Matches(@"^-?\d+(\.\d+)?$")
            .WithMessage("Latitude must be a valid number.");

        RuleFor(geolocation => geolocation.Long)
            .NotEmpty()
            .WithMessage("Longitude is required.")
            .Matches(@"^-?\d+(\.\d+)?$")
            .WithMessage("Longitude must be a valid number.");
    }
}