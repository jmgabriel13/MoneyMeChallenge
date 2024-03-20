using Application.Customers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/customer")]
public class CustomerRateController : ControllerBase
{
    private readonly ICustomerLoanRateService _customerLoanRateService;

    public CustomerRateController(ICustomerLoanRateService customerLoanRateService)
    {
        _customerLoanRateService = customerLoanRateService;
    }

    [HttpPost]
    [Route("rate")]
    public async Task<IActionResult> CustomerRate([FromBody] CreateLoanCustomerRateRequest request, CancellationToken cancellationToken)
    {
        var redirectURL = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}";

        var result = await _customerLoanRateService.Create(request, redirectURL, cancellationToken);

        return Ok(result);
    }
}
