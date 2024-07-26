using CreditCardAuthorization.Api.DTO;
using CreditCardAuthorization.Api.Models.Entities;
using CreditCardAuthorization.Api.Models;
using Microsoft.EntityFrameworkCore;

public class TransactionServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly TransactionService _transactionService;
    private readonly MccService _mccService;

    public TransactionServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _mccService = new MccService(_context);
        _transactionService = new TransactionService(_context, _mccService);
    }

    [Fact]
    public async Task ProcessTransactionAsync_ShouldApproveTransactionUsingFoodBalance()
    {
        // Arrange
        var account = new Account
        {
            Id = "123",
            FoodBalance = 100,
            MealBalance = 0,
            CashBalance = 0
        };
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var transactionDto = new TransactionDTO
        {
            Account = "123",
            TotalAmount = 50,
            MCC = "5411",
            Merchant = "SUPERMERCADO"
        };

        // Act
        var response = await _transactionService.ProcessTransactionAsync(transactionDto);

        // Assert
        Assert.Equal("00", response.Code);
        Assert.Equal(50, account.FoodBalance);
    }

    [Fact]
    public async Task ProcessTransactionAsync_ShouldFallbackToCashBalance()
    {
        // Arrange
        var account = new Account
        {
            Id = "123",
            FoodBalance = 10,
            MealBalance = 0,
            CashBalance = 100
        };
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var transactionDto = new TransactionDTO
        {
            Account = "123",
            TotalAmount = 50,
            MCC = "5411",
            Merchant = "SUPERMERCADO"
        };

        // Act
        var response = await _transactionService.ProcessTransactionAsync(transactionDto);

        // Assert
        Assert.Equal("00", response.Code);
        Assert.Equal(10, account.FoodBalance);
        Assert.Equal(50, account.CashBalance);
    }

    [Fact]
    public async Task ProcessTransactionAsync_ShouldRejectTransactionIfNoSufficientBalance()
    {
        // Arrange
        var account = new Account
        {
            Id = "123",
            FoodBalance = 10,
            MealBalance = 0,
            CashBalance = 30
        };
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var transactionDto = new TransactionDTO
        {
            Account = "123",
            TotalAmount = 50,
            MCC = "5411",
            Merchant = "SUPERMERCADO"
        };

        // Act
        var response = await _transactionService.ProcessTransactionAsync(transactionDto);

        // Assert
        Assert.Equal("51", response.Code);
    }
}
