using System.ComponentModel.DataAnnotations;

namespace CreditCardAuthorization.Api.Models.Entities
{
    public class MerchantMccMapping
    {
        [Key]
        public int Id { get; set; }
        public string MerchantName { get; set; }
        public string Mcc { get; set; }
    }
}
