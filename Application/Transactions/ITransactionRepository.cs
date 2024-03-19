using System.Transactions;

namespace Application.Transactions;
public interface ITransactionRepository
{
    void Add(Transaction transactions);
}
