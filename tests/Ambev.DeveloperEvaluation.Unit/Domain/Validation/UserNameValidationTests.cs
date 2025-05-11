using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Users;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for UserNameValidator.
/// </summary>
public class UserNameValidationTests
{
    private readonly UserNameValidator _validator;

    public UserNameValidationTests()
    {
        _validator = new UserNameValidator();
    }

    [Fact(DisplayName = "Valid name should pass validation")]
    public void Given_ValidUserName_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var username = UserTestData.GenerateValidUserName();

        // Act
        var result = _validator.TestValidate(username);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Empty first name should fail validation")]
    public void Given_EmptyFirstName_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var username = UserTestData.GenerateEmptyFirstName();

        // Act
        var result = _validator.TestValidate(username);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact(DisplayName = "First name with more than 50 characters should fail validation")]
    public void Given_FirstNameWithMoreThan50Characters_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var username = UserTestData.GenerateFirstNameWithMoreThan50characters();

        // Act
        var result = _validator.TestValidate(username);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact(DisplayName = "Empty last name should fail validation")]
    public void Given_EmptyLastName_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var username = UserTestData.GenerateEmptyLastName();

        // Act
        var result = _validator.TestValidate(username);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact(DisplayName = "Last name with more than 50 characters should fail validation")]
    public void Given_LastNameWithMoreThan50Characters_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var username = UserTestData.GenerateLastNameWithMoreThan50characters();

        // Act
        var result = _validator.TestValidate(username);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }
}