namespace CoffeeService.Shared.Payment.MonoPay.Models
{
    public class MerchantPaymInfo
    {
        // The number of the check, order, etc. is determined by the merchant
        public string reference { get; set; } = string.Empty;

        // Payment сomment
        public string destination { get; set; } = string.Empty;

        // items
        public List<BasketItem> basketOrder { get; set; }
    }
}
