using Application.Customers;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/customer")]
public sealed class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerLoanRateService)
    {
        _customerService = customerLoanRateService;
    }

    [HttpGet]
    [Route("loan")]
    public async Task<IActionResult> GetCustomerLoanById(Guid customerId, CancellationToken cancellationToken)
    {
        var result = await _customerService.FindCustomerLoanByIdAsync(customerId, cancellationToken);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("rate")]
    public async Task<IActionResult> CustomerRate([FromBody] CreateLoanCustomerRateRequest request, CancellationToken cancellationToken)
    {
        var redirectURL = $"{Request.Scheme}://{Request.Host}";

        var result = await _customerService.Create(request, redirectURL, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("quote")]
    public async Task<IActionResult> GetCustomerQuote(CustomerQuoteRequest request, CancellationToken cancellationToken)
    {
        var result = await _customerService.CalculateCustomerQuoteAsync(request, cancellationToken);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
