using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class ListUsersHandlerTests
{
    private readonly IUserRepository _UserRepository;
    private readonly IMapper _mapper;
    private readonly GetUsersHandler _handler;

    public ListUsersHandlerTests()
    {
        _UserRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUsersHandler(_UserRepository, _mapper);
    }

    [Fact(DisplayName = "Given two Users when handle result should have count two")]
    public async Task Given_Two_Users_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new GetUsersCommand();

        ICollection<User> users =
        [
            ApplicationUserHandlerTestData.GetValidUser(),
            ApplicationUserHandlerTestData.GetValidUser()
        ];
        _UserRepository.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string?>(), Arg.Any<Dictionary<string, string>>(), Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((users.Count, users.ToList())));
        var user1 = users.ToList()[0];
        var user2 = users.ToList()[1];
        _mapper.Map<List<ApplicationUserResult>>(Arg.Any<List<User>>()).Returns(
        [
                ApplicationUserHandlerTestData.GetApplicationUserResultFromUser(user1),
                ApplicationUserHandlerTestData.GetApplicationUserResultFromUser(user2)
            ]);

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().HaveCount(2);
        queryResult.TotalItems.Should().Be(2);
    }
}