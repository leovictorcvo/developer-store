using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.Common;

/// <summary>
/// Profile for mapping between User entity and UserResult
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<User, ApplicationUserResult>();
    }
}