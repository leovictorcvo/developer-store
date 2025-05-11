using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListUsers operation
    /// </summary>
    public ListUsersProfile()
    {
        CreateMap<ListUsersRequest, GetUsersCommand>()
            .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Order));
    }
}