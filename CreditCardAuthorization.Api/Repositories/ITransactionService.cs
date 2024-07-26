using CreditCardAuthorization.Api.DTO;
using CreditCardAuthorization.Api.Models.Entities;

namespace CreditCardAuthorization.Api.Services
{
    public interface ITransactionService
    {
        Task<Response> ProcessTransactionAsync(TransactionDTO transactionDto);
    }
}