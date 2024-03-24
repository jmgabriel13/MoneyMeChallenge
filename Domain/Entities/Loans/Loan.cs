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
        int termInMonths,
        int amountRequired)
    {
        var loan = new Loan(
            id,
            term,
            termInMonths,
            amountRequired);

        return loan;
    }
}
