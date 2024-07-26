namespace CreditCardAuthorization.Api.DTO
{
    public class TransactionDTO
    {
        public string Account { get; set; }
        public decimal TotalAmount { get; set; }
        public string MCC { get; set; }
        public string Merchant { get; set; }
    }
}
