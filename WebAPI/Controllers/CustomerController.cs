using Application.Customers.CalculateCustomerQuote;
using Application.Customers.CreateCustomerLoanRate;
using Application.Customers.GetCustomerLoanById;
using Application.Customers.UpdateCustomerInfo;
using Application.Customers.UpdateFinanceDetails;
using Application.LoanApplications.CreateLoanApplication;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstraction;

namespace WebApp.Controllers;

[Route("api/customers")]
public sealed class CustomerController : ApiController
{
    [HttpPost]
    [Route("loan/rate")]
    public async Task<IActionResult> CreateCustomerLoanRate([FromBody] CreateCustomerLoanRateRequest request, CancellationToken cancellationToken)
    {
        var redirectURL = $"{Request.Scheme}://{Request.Host}";

        var command = new CreateCustomerLoanRateCommand(
            request.Title,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Mobile,
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

    [HttpGet]
    [Route("calculate/quote")]
    public async Task<IActionResult> CalculateCustomerQuote(CalculateCustomerQuoteRequest request, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("loan/application")]
    public async Task<IActionResult> CreateLoanApplication([FromBody] CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("update/{customerId:guid}")]
    public async Task<IActionResult> UpdateCustomerInfo(Guid customerId, [FromBody] UpdateCustomerInfoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerInfoCommand(
            customerId,
            request.FirstName,
            request.LastName,
            request.Mobile,
            request.Email);

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("loan/update/{customerId:guid}")]
    public async Task<IActionResult> UpdateCustomerFinanceDetails(Guid customerId, [FromBody] UpdateFinanceDetailsRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateFinanceDetailsCommand(
            customerId,
            request.AmountRequired,
            request.TermInMonths);

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
