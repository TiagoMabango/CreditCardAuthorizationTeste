using CreditCardAuthorization.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAuthorization.Api.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MerchantMccMapping> MerchantMccMappings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
