using CreditCardAuthorization.Api.Models.Entities;
using System.Threading.Tasks;

namespace CreditCardAuthorization.Api.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Account> GetAccountByIdAsync(string accountId);
        Task SaveChangesAsync();
    }
}
