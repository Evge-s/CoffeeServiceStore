namespace CoffeeService.Server.Services.PaymentService.Mono
{
    public interface IMonoService
    {
        Task<HttpResponseMessage> CreateChecoutSession();
    }
}