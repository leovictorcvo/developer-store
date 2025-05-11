using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Users;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class UserAddressValidatorTest
{
    private readonly UserAddressValidator _validator;

    public UserAddressValidatorTest()
    {
        _validator = new UserAddressValidator();
    }

    [Fact(DisplayName = "Valid user address should pass validation")]
    public void Given_ValidUserAddress_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var address = UserAddressTestData.GenerateValid();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Empty street should fail validation")]
    public void Given_EmptyStreet_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateEmptyAddressStreet();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Fact(DisplayName = "Big street should fail validation")]
    public void Given_BigStreet_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateBigAddressStreet();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Fact(DisplayName = "Empty city should fail validation")]
    public void Given_EmptyCity_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateEmptyAddressCity();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact(DisplayName = "Big city should fail validation")]
    public void Given_BigCity_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateBigAddressCity();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact(DisplayName = "Empty number should fail validation")]
    public void Given_EmptyNumber_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateEmptyAddressNumber();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }

    [Fact(DisplayName = "Empty zip code should fail validation")]
    public void Given_EmptyZipCode_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateEmptyAddressZipCode();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode);
    }

    [Fact(DisplayName = "Invalid zip code format should fail validation")]
    public void Given_InvalidZipCodeFormat_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var address = UserAddressTestData.GenerateInvalidAddressZipCode();

        // Act
        var result = _validator.TestValidate(address);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode);
    }
}