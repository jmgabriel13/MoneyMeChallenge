using Application.LoanApplications.CreateLoanApplication;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstraction;

namespace WebApp.Controllers;

[Route("api/loan")]
public sealed class LoanApplicationController : ApiController
{
    [HttpPost]
    [Route("application")]
    public async Task<IActionResult> LoanApplication([FromBody] CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
