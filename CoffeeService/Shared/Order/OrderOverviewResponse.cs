namespace CoffeeService.Shared.Order
{
    public class OrderOverviewResponse
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Product { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
