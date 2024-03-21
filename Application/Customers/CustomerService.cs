using Application.Interfaces;
using Domain.Entities.Customers;
using Domain.Entities.Loans;

namespace Application.Customers;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken)
    {
        bool isValidDate = DateTime.TryParse(request.DateOfBirth, out DateTime parsedDate);
        if (!isValidDate)
        {
            // Return 
            return "Date of Birth is not valid.";
        }

        // Check if firstName, lastName and dateOfBirth is existing, then return same redirect URL
        var customer = await _customerRepository.GetByFirstLastNameAndDateOfBirthAsync(
            request.FirstName,
            request.LastName,
            parsedDate,
            cancellationToken);

        if (customer is not null)
        {
            // If customer is existing using firstName, lastName and dateOfbirth, return same redirectUrl
            return customer.RedirectURL;
        }

        // Creating instance of loan
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            Term = Int32.Parse(request.Term),
            AmountRequired = Int32.Parse(request.AmountRequired)
        };

        // Creating new customer
        var newCustomerId = Guid.NewGuid();
        var newCustomer = new Customer
        {
            Id = newCustomerId,
            Title = request.Title,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = parsedDate,
            MobileNumber = request.MobileNumber,
            Email = request.Email,
            RedirectURL = $"{redirectUrl}/quote-calculator?id={newCustomerId}",
            Loan = loan
        };

        _customerRepository.Add(newCustomer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newCustomer.RedirectURL;
    }

    public async Task<CustomerLoanResponse?> FindCustomerLoanByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.FindByIdAsync(customerId, cancellationToken);

        if (customer is null)
        {
            throw new InvalidOperationException("Customer not found");
        }

        // Customer response object
        var response = new CustomerLoanResponse(
            customer.Title,
            customer.FirstName,
            customer.LastName,
            customer.DateOfBirth,
            customer.MobileNumber,
            customer.Email,
            customer.Loan.Term,
            customer.Loan.AmountRequired);

        return response;
    }
}
