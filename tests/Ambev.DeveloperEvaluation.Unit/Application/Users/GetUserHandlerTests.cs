using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class GetUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUserHandler(_userRepository, _mapper);
    }

    [Fact(DisplayName = "Given invalid User identifier throws ResourceNotFoundException")]
    public async Task Handle_InvalidRequest_ThrowsResourceNotFoundException()
    {
        Guid requestedBy = ApplicationUserHandlerTestData.GetValidUserId();
        UserRole applicantRole = ApplicationUserHandlerTestData.GetValidUserRole();
        var command = new GetUserCommand(Guid.Empty, requestedBy, applicantRole);

        var method = () => _handler.Handle(command, CancellationToken.None);

        await method.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid User identifier returns User")]
    public async Task Handle_ValidRequest_ReturnsUser()
    {
        User user = ApplicationUserHandlerTestData.GetValidUser();
        ApplicationUserResult? result = null;
        Guid requestedBy = ApplicationUserHandlerTestData.GetValidUserId();
        UserRole applicantRole = ApplicationUserHandlerTestData.GetValidUserRole();
        var command = new GetUserCommand(user.Id, requestedBy, applicantRole);
        _userRepository.GetByIdAsync(command.Id, true, requestedBy, applicantRole, CancellationToken.None)
            .Returns(Task.FromResult(user));
        _mapper.Map<ApplicationUserResult>(user).Returns(ApplicationUserHandlerTestData.GetApplicationUserResultFromUser(user));

        var method = async () => result = await _handler.Handle(command, CancellationToken.None);

        await method.Should().NotThrowAsync();
        result.Should().NotBeNull();
        result!.Id.Should().Be(user.Id);
        result!.Username.Should().Be(user.Username);
        result!.Email.Should().Be(user.Email);
    }
}