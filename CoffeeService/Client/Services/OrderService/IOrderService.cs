namespace CoffeeService.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<string> MonoPlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
        Task<OrderDetailsResponse> GetOrderDetails(int orderId);
    }
}
