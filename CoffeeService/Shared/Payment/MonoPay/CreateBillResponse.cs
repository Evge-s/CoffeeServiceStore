namespace CoffeeService.Shared.Payment.MonoPay
{
    public class CreateBillResponse
    {
        public string invoiceId { get; set; } = string.Empty;
        public string pageUrl { get; set; } = string.Empty;
    }
}
