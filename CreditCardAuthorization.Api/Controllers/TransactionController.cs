using CreditCardAuthorization.Api.DTO;
using CreditCardAuthorization.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAuthorization.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionDTO transactionDto)
        {
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);
            return Ok(result);
        }
    }
}
