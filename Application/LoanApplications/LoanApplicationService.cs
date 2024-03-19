using Application.Customers;
using Application.Interfaces;
using Application.Products;
using Domain.Entities.LoanApplications;
using Microsoft.VisualBasic;

namespace Application.LoanApplications;
public class LoanApplicationService
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

    public async Task Create(CreateLoanApplicationRequest request, string redirectUrl, CancellationToken cancellationToken)
    {
        // Get product by Id
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            // Return product not found
            return;
        }

        var customer = await _customerRepository.FindByIdAsync(request.CustomerId);

        if (customer is null)
        {
            // Return customer not found
            return;
        }

        // calculate the monthly repayment amount using PMT function
        double monthlyRate = (double)product.InterestRate / 100 / 12;
        double monthlyPayment = -Financial.Pmt(monthlyRate, customer.Loan.Term, customer.Loan.AmountRequired);

        // Creating instance of loanApplication
        var loanApplication = new LoanApplicaton
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            TotalRepayments = (decimal)monthlyPayment * 12,
            MonthlyRepayment = (decimal)monthlyPayment,
            InterestRate = product.InterestRate,
            IsApproved = false
        };

        _loanApplicationRepository.Add(loanApplication);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return;
    }
}
