using Application.Customers.CalculateCustomerQuote;
using Application.Customers.CreateCustomerLoanRate;
using Application.Customers.GetCustomerLoanById;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstraction;

namespace WebApp.Controllers;

[Route("api/customers")]
public sealed class CustomerController : ApiController
{
    [HttpGet]
    [Route("loan/{customerId:guid}")]
    public async Task<IActionResult> GetCustomerLoanById(Guid customerId, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(new GetCustomerLoanByIdQuery(customerId), cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("rate")]
    public async Task<IActionResult> CustomerRate([FromBody] CreateCustomerLoanRateRequest request, CancellationToken cancellationToken)
    {
        var redirectURL = $"{Request.Scheme}://{Request.Host}";

        var command = new CreateCustomerLoanRateCommand(
            request.Title,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.MobileNumber,
            request.Email,
            request.Term,
            request.AmountRequired,
            redirectURL);

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("quote")]
    public async Task<IActionResult> GetCustomerQuote(CalculateCustomerQuoteRequest request, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
