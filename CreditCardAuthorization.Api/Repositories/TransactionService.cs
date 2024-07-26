using CreditCardAuthorization.Api.DTO;
using CreditCardAuthorization.Api.Models;
using CreditCardAuthorization.Api.Models.Entities;
using CreditCardAuthorization.Api.Services;
using Microsoft.EntityFrameworkCore;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;
    private readonly MccService _mccService;

    public TransactionService(ApplicationDbContext context, MccService mccService)
    {
        _context = context;
        _mccService = mccService;
    }

    public async Task<Response> ProcessTransactionAsync(TransactionDTO transactionDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var account = await _context.Accounts
                .FromSqlInterpolated($"SELECT * FROM Accounts WITH (UPDLOCK) WHERE Id = {transactionDto.Account}")
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return new Response { Code = "07" };
            }

            var effectiveMcc = await _mccService.GetMappedMccAsync(transactionDto.Merchant, transactionDto.MCC);
            var balanceType = GetBalanceType(effectiveMcc);

            if (balanceType == "FOOD" && account.FoodBalance >= transactionDto.TotalAmount)
            {
                account.FoodBalance -= transactionDto.TotalAmount;
            }
            else if (balanceType == "MEAL" && account.MealBalance >= transactionDto.TotalAmount)
            {
                account.MealBalance -= transactionDto.TotalAmount;
            }
            else if (account.CashBalance >= transactionDto.TotalAmount)
            {
                account.CashBalance -= transactionDto.TotalAmount;
            }
            else
            {
                return new Response { Code = "51" };
            }

            var t = new Transaction
            {
                AccountId = transactionDto.Account,
                Amount = transactionDto.TotalAmount,
                Merchant = transactionDto.Merchant,
                MCC = transactionDto.MCC
            };

            _context.Transactions.Add(t);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new Response { Code = "00" };
        }
        catch
        {
            await transaction.RollbackAsync();
            return new Response { Code = "07" };
        }
    }

    private string GetBalanceType(string mcc)
    {
        return mcc switch
        {
            "5411" or "5412" => "FOOD",
            "5811" or "5812" => "MEAL",
            _ => "CASH",
        };
    }
}
