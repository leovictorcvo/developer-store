using System.Security.Claims;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Base controller for all controllers in the application.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Gets the current user ID from the claims.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    protected Guid GetCurrentUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();
        var idOk = Guid.TryParse(userIdString, out var userId);
        if (!idOk)
            throw new UnauthorizedAccessException($"User ID {userIdString} is not valid");
        return userId;
    }

    /// <summary>
    /// Gets the current user email from the claims.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizedAccessException();

    /// <summary>
    /// Gets the current user role from the claims.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    protected UserRole GetCurrentUserRole()
    {
        var roleString = User.FindFirst(ClaimTypes.Role)?.Value ?? throw new UnauthorizedAccessException();
        var roleOk = Enum.TryParse<UserRole>(roleString, out var role);
        if (!roleOk)
            throw new UnauthorizedAccessException($"Role {roleString} is not valid");
        return role;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseController"/> class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    protected IActionResult OkWithData<T>(T data) =>
            base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });

    /// <summary>
    /// Creates a new instance of the <see cref="BaseController"/> class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="routeName"></param>
    /// <param name="routeValues"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    /// <summary>
    /// Creates a new instance of the <see cref="BaseController"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    /// <summary>
    /// Creates a new instance of the <see cref="BaseController"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    /// <summary>
    /// Creates a new instance of the <see cref="BaseController"/> class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pagedList"></param>
    /// <returns></returns>
    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
            Ok(new PaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                Success = true
            });
}