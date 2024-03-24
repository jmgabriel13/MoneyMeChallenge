using Domain.Shared;

namespace Domain.Errors;
public static class DomainErrors
{
    public static class Product
    {
        public static Error ProductNotFound(Guid id) => new("Product.NotFound", $"The product with the ID = {id} was not found.");
    }

    public static class Customer
    {
        public static Error CustomerNotFound(Guid id) => new("Customer.NotFound", $"Customer with id = {id} was not found.");

        public static readonly Error InvalidDateOfBirth = new("Customer.DateOfBirth", "Date of Birth is not valid.");
        public static readonly Error CustomerOrProductNotFound = new("Customer.Calculate.Quote", "Customer or product not found.");
        public static readonly Error HasPendingLoanApplication = new("Customer.Loan.Application", "Customer Has pending loan application.");
        public static readonly Error NotEligible = new("Customer.Loan.Application", "Customer is not eligible to apply for a loan.");
    }
}
