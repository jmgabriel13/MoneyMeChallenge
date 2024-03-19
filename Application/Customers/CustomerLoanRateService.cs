using Application.Interfaces;
using Domain.Entities.Customers;
using Domain.Entities.Loans;

namespace Application.Customers;
public class CustomerLoanRateService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerLoanRateService(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken)
    {
        // Check if firstName, lastName and dateOfBirth is existing, then return same redirect URL
        var customer = await _customerRepository.GetByFirstLastNameAndDateOfBirthAsync(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            cancellationToken);

        if (customer is not null)
        {
            // If customer is existing using firstName, lastName and dateOfbirth, return same redirectUrl
            return customer.RedirectURL;
        }

        // Creating new customer
        var newCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            MobileNumber = request.MobileNumber,
            Email = request.Email,
            RedirectURL = redirectUrl
        };

        // Creating instance of loan
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            Term = request.Term,
            AmountRequired = request.AmountRequired
        };

        _customerRepository.Add(newCustomer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newCustomer.RedirectURL;
    }

}
