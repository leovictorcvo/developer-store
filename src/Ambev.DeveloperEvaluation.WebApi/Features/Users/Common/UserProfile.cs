using Ambev.DeveloperEvaluation.Application.Users.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;

/// <summary>
/// Profile for mapping between Application and API User responses
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUserResult, UserResponse>();
    }
}