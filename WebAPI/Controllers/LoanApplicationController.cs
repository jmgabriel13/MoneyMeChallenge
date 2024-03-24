using Application.LoanApplications;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/loan")]
public sealed class LoanApplicationController : Controller
{
    private readonly ILoanApplicationService _loanApplicationService;

    public LoanApplicationController(ILoanApplicationService loanApplicationService)
    {
        _loanApplicationService = loanApplicationService;
    }

    [HttpPost]
    [Route("application")]
    public async Task<IActionResult> LoanApplication([FromBody] CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        await _loanApplicationService.Create(request, cancellationToken);

        return Ok();
    }
}
