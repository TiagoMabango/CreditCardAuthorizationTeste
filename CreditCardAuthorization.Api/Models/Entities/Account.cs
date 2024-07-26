using System.ComponentModel.DataAnnotations;

namespace CreditCardAuthorization.Api.Models.Entities
{
    public class Account
    {
        [Key]
        public string Id { get; set; }
        public decimal FoodBalance { get; set; }
        public decimal MealBalance { get; set; }
        public decimal CashBalance { get; set; }
    }
}
