using System.ComponentModel.DataAnnotations;

namespace CreditCardAuthorization.Api.Models.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Merchant { get; set; }

        [Required]
        public string MCC { get; set; }

    }
}
