using Application.Customers;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerLoanRateService)
    {
        _customerService = customerLoanRateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _customerService.FindByIdAsync(id, cancellationToken);

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
}
