using CreditCardAuthorization.Api.Models.Entities;
using CreditCardAuthorization.Api.Models;
using Microsoft.EntityFrameworkCore;

public class MccServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly MccService _mccService;

    public MccServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _mccService = new MccService(_context);
    }

    [Fact]
    public async Task GetMappedMccAsync_ShouldReturnMappedMcc()
    {
        // Arrange
        _context.MerchantMccMappings.Add(new MerchantMccMapping { MerchantName = "UBER EATS", Mcc = "5812" });
        await _context.SaveChangesAsync();

        // Act
        string mappedMcc = await _mccService.GetMappedMccAsync("UBER EATS SAO PAULO BR", "0000");

        // Assert
        Assert.Equal("5812", mappedMcc);
    }

    [Fact]
    public async Task GetMappedMccAsync_ShouldReturnOriginalMccIfNoMappingFound()
    {
        // Arrange
        _context.MerchantMccMappings.Add(new MerchantMccMapping { MerchantName = "UBER EATS", Mcc = "5812" });
        await _context.SaveChangesAsync();

        // Act
        string mappedMcc = await _mccService.GetMappedMccAsync("OTHER MERCHANT", "0000");

        // Assert
        Assert.Equal("0000", mappedMcc);
    }
}
