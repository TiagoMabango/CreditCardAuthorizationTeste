using CreditCardAuthorization.Api.Models;
using Microsoft.EntityFrameworkCore;

public class MccService
{
    private readonly ApplicationDbContext _context;

    public MccService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetMappedMccAsync(string merchantName, string originalMcc)
    {
        var mapping = await _context.MerchantMccMappings
            .FirstOrDefaultAsync(m => merchantName.Contains(m.MerchantName));

        return mapping != null ? mapping.Mcc : originalMcc;
    }
}