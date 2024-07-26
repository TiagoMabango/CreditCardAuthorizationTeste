using CreditCardAuthorization.Api.Models;
using CreditCardAuthorization.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAuthorization.Api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly MccService _mccService;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Account> GetAccountByIdAsync(string accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
