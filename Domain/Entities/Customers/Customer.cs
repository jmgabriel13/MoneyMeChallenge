using Domain.Entities.Loans;

namespace Domain.Entities.Customers;
public class Customer
{
    private const string _url = "/quote-calculator?customerId=";

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Mobile { get; private set; }
    public string Email { get; private set; }
    public string RedirectURL { get; private set; }
    public Loan Loan { get; private set; }

    private Customer(
        Guid id,
        string title,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string mobileNumber,
        string email,
        string redirectURL,
        Loan loan)
    {
        Id = id;
        Title = title;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Mobile = mobileNumber;
        Email = email;
        RedirectURL = redirectURL;
        Loan = loan;
    }

    private Customer()
    {
    }

    // Use static factory method approach to create a new order instance
    public static Customer Create(
        Guid id,
        string title,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string mobileNumber,
        string email,
        string redirectURL,
        string term,
        string amountRequired)
    {
        // Creating instance of loan
        var loan = Loan.Create(
            Guid.NewGuid(),
            decimal.Parse(term),
            YearsToMonths(decimal.Parse(term)),
            Int32.Parse(amountRequired));

        // Generate redirect link for current customer
        var redirectUrlLink = $"{redirectURL}{_url}{id}";

        var customer = new Customer(
                id,
                title,
                firstName,
                lastName,
                dateOfBirth,
                mobileNumber,
                email,
                redirectUrlLink,
                loan);

        return customer;
    }

    private static int YearsToMonths(decimal years)
    {
        if (years < 0)
        {
            throw new ArgumentException("Invalid input: Please provide a non-negative number of years.", nameof(years));
        }

        return (int)Math.Round(years * 12);
    }
}
