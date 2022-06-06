using CoffeeService.Shared.Payment.MonoPay.Models;

namespace CoffeeService.Shared.Payment.MonoPay
{
    public class CreateBillRequest
    {
        // The amount of payment in minimum units(a penny for hryvnia)
        public Int64 amount { get; set; }

        // ISO 4217 Currency Code, default 980 (hryvnia)
        public Int32 ccy { get; set; } = 980;

        // Cart objects
        public MerchantPaymInfo merchantPaymInfo { get; set; }

        // Return Address (GET) - The user will be forwarded to this address upon completion of payment (in case of success or error)
        public string redirectUrl { get; set; }

        // Callback address (Post) - This address will be sent to this payment status for each status change.
        // The content of the body of the query is identical to the answer of the request “verification of account status”.
        public string webHookUrl { get; set; } = string.Empty;

        // The validity period in seconds, default account ceases to be valid in 24 hours
        // 15min in current case
        public Int64 validity { get; set; } = 1800;

        public string paymentType { get; set; } = string.Empty;

        public string qrId { get; set; } = string.Empty;
    }
}
