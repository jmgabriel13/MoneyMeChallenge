using Domain.Entities.Customers;

namespace Domain.Entities.Loans;
public class Loan
{
    private Loan(Guid id, decimal term, int termInMonths, int amountRequired)
    {
        Id = id;
        Term = term;
        TermInMonths = termInMonths;
        AmountRequired = amountRequired;
    }

    private Loan()
    {
    }

    public Guid Id { get; private set; }
    public decimal Term { get; private set; }
    public int TermInMonths { get; private set; }
    public int AmountRequired { get; private set; }

    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    public static Loan Create(
        Guid id,
        decimal term,
        int amountRequired)
    {
        var loan = new Loan(
            id,
            term,
            YearsToMonths(term),
            amountRequired);

        return loan;
    }

    public void Update(int termInMonths, int amountRequired)
    {
        Term = MonthsToYears((int)termInMonths);
        TermInMonths = termInMonths;
        AmountRequired = amountRequired;
    }

    private static int YearsToMonths(decimal years)
    {
        if (years < 0)
        {
            throw new ArgumentException("Invalid input: Please provide a non-negative number of years.", nameof(years));
        }

        return (int)Math.Round(years * 12);
    }

    public static decimal MonthsToYears(int months)
    {
        if (months < 0)
        {
            throw new ArgumentException("Invalid input: Please provide a non-negative number of months.", nameof(months));
        }

        return (decimal)months / 12;
    }
}
