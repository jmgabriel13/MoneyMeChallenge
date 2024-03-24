using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Abstraction;

public class ApiController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult HandleFailure(Result result, Guid id = default) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(), // dont call this method if successful.
            _ => // wild card here to just return the bad request.
                BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    // return the new object that matches the problem details specification
    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
