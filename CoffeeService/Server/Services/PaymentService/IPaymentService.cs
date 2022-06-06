namespace CoffeeService.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<HttpResponseMessage> CreateChecoutSession();
    }
}