using System.Net;
using System.Text.Json;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(context, ex);
            }
            finally
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await ExceptionHandlerAsync(context, new UnauthorizedAccessException("Invalid credentials"));
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    // Verificar o endpoint e a política/role que falhou
                    var endpoint = context.GetEndpoint();
                    var authorizeData = endpoint?.Metadata.GetMetadata<IAuthorizeData>();

                    string errorMessage = "Acess denied.";

                    if (authorizeData?.Roles != null)
                    {
                        errorMessage = $"This endpoint can only be accessed by users in the group(s) {authorizeData.Roles}.";
                    }
                    await ExceptionHandlerAsync(context, new ForbiddenAccessException(errorMessage));
                }
            }
        }

        private static Task ExceptionHandlerAsync(HttpContext context, Exception ex)
        {
            var response = new ApiResponse { Success = false };
            int statusCode = StatusCodes.Status400BadRequest;

            switch (ex)
            {
                case ForbiddenAccessException:
                    response.Message = "UnauthorizedAccessException";
                    response.Errors = [new() { Error = "Access Denied Exception", Detail = ex.Message }];
                    statusCode = StatusCodes.Status403Forbidden;
                    break;

                case UnauthorizedAccessException:
                    response.Message = "UnauthorizedAccessException";
                    response.Errors = [new() { Error = "Unauthorized Access Exception", Detail = ex.Message }];
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;

                case ArgumentException:
                    response.Message = "ArgumentException";
                    response.Errors = [new() { Error = "Argument Exception", Detail = ex.Message }];
                    break;

                case ResourceNotFoundException:
                    ResourceNotFoundException rnfex = (ResourceNotFoundException)ex;
                    response.Message = "ResourceNotFound";
                    response.Errors = [new() { Error = rnfex.Error, Detail = rnfex.Detail }];
                    break;

                case DomainException:
                    response.Message = "DomainException";
                    response.Errors = [new() { Error = "Domain Exception", Detail = ex.Message }];
                    break;

                case InvalidOperationException:
                    response.Message = "InvalidOperationException";
                    response.Errors = [new() { Error = "Invalid Operation", Detail = ex.Message }];
                    break;

                case ValidationException:
                    ValidationException vex = (ValidationException)ex;
                    response.Message = "ValidationException";
                    response.Errors = vex.Errors.Select(error => (ValidationErrorDetail)error);
                    break;

                default:
                    response.Message = "Exception";
                    response.Errors = [new() { Error = "Internal Server Error", Detail = ex?.Message ?? "An unexpected error occurred." }];
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return WriteExceptionResponseAsync(context, statusCode, response);
        }

        private static readonly JsonSerializerOptions _writeOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static Task WriteExceptionResponseAsync(
            HttpContext context,
            int statusCode,
            ApiResponse response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, _writeOptions));
        }
    }
}