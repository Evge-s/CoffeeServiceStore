namespace CoffeeService.Shared.Order
{
    public class OrderDetailsResponse
    {
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsProductResponse> Products { get; set; }
    }
}
