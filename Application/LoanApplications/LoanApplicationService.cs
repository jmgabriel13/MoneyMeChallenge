using Application.Customers;
using Application.Interfaces;
using Application.Products;
using Domain.Entities.LoanApplications;
using Microsoft.VisualBasic;

namespace Application.LoanApplications;
public class LoanApplicationService : ILoanApplicationService
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoanApplicationService(
        ILoanApplicationRepository loanApplicationRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        ICustomerRepository customerRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task Create(CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (customer is null || product is null)
        {
            // Return error, cant proceed if customer or product are not found
            return;
        }

        // calculate the monthly repayment amount using PMT function
        double monthlyRate = (double)product.PerAnnumInterestRate / 100 / (int)RepaymentFrequency.Monthly;
        double monthlyPayment = -Financial.Pmt(
            monthlyRate,
            customer.Loan.Term * (int)RepaymentFrequency.Monthly,
            customer.Loan.AmountRequired);
        decimal totalRepayments = (decimal)monthlyPayment * (customer.Loan.Term * (int)RepaymentFrequency.Monthly);

        // Creating instance of loanApplication
        var loanApplication = new LoanApplicaton
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            RepaymentFrequency = RepaymentFrequency.Monthly.ToString(),
            Repayment = (decimal)monthlyPayment,
            TotalRepayments = totalRepayments,
            InterestRate = product.PerAnnumInterestRate / (int)RepaymentFrequency.Monthly,
            Interest = totalRepayments - customer.Loan.AmountRequired,
            Status = LoanStatus.Pending
        };

        // Add establishmentFee to TotalRepayments
        loanApplication.TotalRepayments += loanApplication.EstablishmentFee;

        _loanApplicationRepository.Add(loanApplication);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return;
    }
}
