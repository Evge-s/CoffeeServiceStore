using CoffeeService.Shared.Payment.MonoPay.Models;

namespace CoffeeService.Server.Services.PaymentService.Mono
{
    public class MonoService : IMonoService
    {
        private readonly ICartService _cartService;
        private readonly IConfiguration _iConfig;
        private readonly HttpClient _httpClient = new HttpClient();
        private const string TokenHeader = "X-Token";
        private readonly string ApiKey;

        public MonoService(ICartService cartService,
            IConfiguration iConfig)
        {
            _cartService = cartService;
            _iConfig = iConfig;
            ApiKey = _iConfig.GetSection("AppSettings").GetSection(TokenHeader).Value;
        }

        public async Task<HttpResponseMessage> CreateChecoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            decimal orderPrice = 0;
            var basketOrderResult = new List<BasketItem>();
            products.ForEach(product => basketOrderResult.Add(new BasketItem
            {
                name = product.Title,
                icon = product.ImageUrl,
                qty = Convert.ToInt32(product.Quantity),
                unit = product.ProductType,
                sum = Convert.ToInt64(product.Price * 100)
            }));

            products.ForEach(product => orderPrice += product.Price * product.Quantity);

            var session = new CreateBillRequest
            {
                amount = Convert.ToInt64(orderPrice * 100),
                ccy = 980,
                merchantPaymInfo = new MerchantPaymInfo
                {
                    reference = "1",                      // PASS
                    destination = "purchase",
                    basketOrder = basketOrderResult
                },
                redirectUrl = "https://localhost:7234/order-success"
            };
            _httpClient.DefaultRequestHeaders.Add(TokenHeader, ApiKey);
            return await _httpClient.PostAsJsonAsync("https://api.monobank.ua/api/merchant/invoice/create", session);
        }
    }
}
