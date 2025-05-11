using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Handler for processing ListUsersCommand requests
    /// </summary>
    public class GetUsersHandler : IRequestHandler<GetUsersCommand, PaginationResult<ApplicationUserResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ListUsersHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the ListUsersCommand request
        /// </summary>
        /// <param name="command">The ListUsers command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The list of users</returns>
        public async Task<PaginationResult<ApplicationUserResult>> Handle(GetUsersCommand command, CancellationToken cancellationToken)
        {
            (int totalUsers, List<User> users) = await _userRepository.GetAllAsync(command.Page, command.Size, command.Sort, command.Filters, command.RequestedBy, command.ApplicantRole, cancellationToken);

            return new PaginationResult<ApplicationUserResult>()
            {
                Page = command.Page,
                Size = command.Size,
                TotalItems = totalUsers,
                Items = _mapper.Map<List<ApplicationUserResult>>(users)
            };
        }
    }
}