using Application.Customers;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstraction;

namespace WebApp.Controllers;

[Route("api/customers")]
public sealed class CustomerController(ICustomerService _customerService) : ApiController
{
    [HttpGet]
    [Route("loan/{customerId:guid}")]
    public async Task<IActionResult> GetCustomerLoanById(Guid customerId, CancellationToken cancellationToken)
    {
        Result result = await _customerService.FindCustomerLoanByIdAsync(customerId, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("rate")]
    public async Task<IActionResult> CustomerRate([FromBody] CreateLoanCustomerRateRequest request, CancellationToken cancellationToken)
    {
        var redirectURL = $"{Request.Scheme}://{Request.Host}";

        Result result = await _customerService.Create(request, redirectURL, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("quote")]
    public async Task<IActionResult> GetCustomerQuote(CustomerQuoteRequest request, CancellationToken cancellationToken)
    {
        Result result = await _customerService.CalculateCustomerQuoteAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
