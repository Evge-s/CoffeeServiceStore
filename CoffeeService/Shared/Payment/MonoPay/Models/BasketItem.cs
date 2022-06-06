namespace CoffeeService.Shared.Payment.MonoPay.Models
{
    public class BasketItem
    {
        public string name { get; set; } = string.Empty;
        public Int32 qty { get; set; }
        public Int64 sum { get; set; }
        public string icon { get; set; } = string.Empty;
        public string unit { get; set; } = string.Empty;
    }
}
